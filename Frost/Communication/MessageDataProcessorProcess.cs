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
        public Message Process(Message message)
        {
            Message response = new Message();
            switch (message.Action)
            {
                case MessageDataAction.Process.Get_Remote_Row:
                    response = ProcessGetRemoteRow(message);
                    break;
                case MessageDataAction.Process.Remote_Row_Information:
                    response = ProcessRemoteRowInformation(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Process Data Message");
            }
            return response;
        }
        #endregion

        #region Private Methods
        private Message ProcessRemoteRowInformation(Message message)
        {
            throw new NotImplementedException();
        }
        private Message ProcessGetRemoteRow(Message message)
        {

            // get are getting a request for a remote row. We need to find the row and then return it to the requestor.
            // when we send it back, we send it as Remote_Row_Information = "Process.Remote_Row_Information" as the action
            // when we NEW up a message, we need to set the RequestInfoId to the message we got from the GetRmote_RowInformation

            RemoteRowInfo info = message.GetContentAs<RemoteRowInfo>();
            Row row = new Row(); // TO DO: need to actually find the row
            row = GetRow(info, row);
            var content = JsonConvert.SerializeObject(row);
            return new Message(message.Origin, _process.GetLocation(), content, MessageDataAction.Process.Remote_Row_Information, MessageType.Data, message.RequestInformationId);
            
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
