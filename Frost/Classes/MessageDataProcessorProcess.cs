using FrostCommon;
using FrostCommon.DataMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FrostDB
{
    public class MessageDataProcessorProcess 
    {

        #region Private Fields
        Process _process;
        #endregion

        #region Public Properties
        public int PortNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessorProcess(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageDataAction.Process.Get_Remote_Row:
                    ProcessGetRemoteRow(message);
                    break;
                case MessageDataAction.Process.Remote_Row_Information:
                    ProcessRemoteRowInformation(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Process Data Message");
            }
        }
        #endregion

        #region Private Methods
        private void ProcessRemoteRowInformation(Message message)
        {
            // someone has sent us information that we requested for a remote row. we need to figure out who asked for this information and
            // return to call site

            if (message.RequestInformationId != null)
            {
                // singal that we got the data
                _process.Network.RemoveFromQueueToken(message.RequestInformationId);
                // add it to the dictionary so that the caller can parse it
                _process.Network.DataProcessor.IncomingMessages.TryAdd(message.RequestInformationId, message);

            }

            string debug = $"ProcessRemoteRowInformation";
            Debug.WriteLine(debug);
        }
        private void ProcessGetRemoteRow(Message message)
        {

            // get are getting a request for a remote row. We need to find the row and then return it to the requestor.
            // when we send it back, we send it as Remote_Row_Information = "Process.Remote_Row_Information" as the action
            // when we NEW up a message, we need to set the RequestInfoId to the message we got from the GetRmote_RowInformation

            RemoteRowInfo info = message.GetContentAs<RemoteRowInfo>();

            Row row = new Row(); // TO DO: need to actually find the row

            row = GetRow(info, row);

            var content = JsonConvert.SerializeObject(row);
            var rowDataMessage = new Message(message.Origin, _process.GetLocation(), content, MessageDataAction.Process.Remote_Row_Information, MessageType.Data, message.RequestInformationId);
            _process.Network.SendMessage(rowDataMessage);
        }

        private Row GetRow(RemoteRowInfo info, Row row)
        {
            if (_process.HasPartialDatabase(info.DatabaseName))
            {
                var db = _process.GetPartialDatabase(info.DatabaseName);
                if (db.HasTable(info.TableName))
                {
                    var table = db.GetTable(info.TableName);
                    row = table.GetRow(info.RowId);
                }
            }

            return row;
        }
        #endregion




    }
}
