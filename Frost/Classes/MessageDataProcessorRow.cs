using FrostCommon;
using FrostCommon.DataMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FrostDB.Classes
{
    public class MessageDataProcessorRow
    {

        #region Private Fields
        private Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public MessageDataProcessorRow(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageDataAction.Row.Save_Row:
                    ProcessSaveRow(message);
                    break;
                case MessageDataAction.Row.Delete_Row:
                    ProcessDeleteRow(message);
                    break;
                case MessageDataAction.Row.Update_Row:
                    ProcessUpdateRow(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Data Row Message");
            }
        }
        #endregion

        #region Private Methods
        private async Task ProcessUpdateRow(Message message)
        {
            var info = message.GetContentAs<RowForm>();
            if (_process.HasPartialDatabase(info.DatabaseName))
            {
                var db = _process.GetPartialDatabase(info.DatabaseName);
                if (db.HasTable(info.TableName))
                {
                    var table = db.GetTable(info.TableName);
                    await table.UpdateRow(info.Reference, info.RowValues);
                    var returnMessage = _process.Network.BuildMessage(message.Origin, null, MessageDataAction.Row.Update_Row_Information, MessageType.Data, message.RequestInformationId);
                    _process.Network.SendMessage(returnMessage);
                }
            }
        }

        private void ProcessDeleteRow(Message message)
        {
            // TO DO: We should be checking the contract here if the host is allowed to delete our data;

            var info = message.GetContentAs<RemoteRowInfo>();
            if (_process.HasPartialDatabase(info.DatabaseName))
            {
                var db = _process.GetPartialDatabase(info.DatabaseName);
                if (db.HasTable(info.TableName))
                {
                    var table = db.GetTable(info.TableName);
                    table.RemoveRow(info.RowId);
                }
            }
        }
        private void ProcessSaveRow(Message message)
        {
            var info = JsonConvert.DeserializeObject<RowForm>(message.Content);
            if (_process.HasPartialDatabase(info.DatabaseName))
            {
                var db = _process.GetPartialDatabase(info.DatabaseName);
                if (db.HasTable(info.TableName))
                {
                    var table = db.GetTable(info.TableName);
                    table.AddRow(info);
                }
            }
        }
        #endregion

    }
}
