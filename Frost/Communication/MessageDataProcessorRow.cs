using FrostCommon;
using FrostCommon.DataMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public IMessage Process(Message message)
        {
            IMessage result = null;
            switch (message.Action)
            {
                case MessageDataAction.Row.Save_Row:
                    result = ProcessSaveRow(message);
                    break;
                case MessageDataAction.Row.Delete_Row:
                    result = ProcessDeleteRow(message);
                    break;
                case MessageDataAction.Row.Update_Row:
                    result = ProcessUpdateRow(message).Result;
                    break;
                default:
                    throw new InvalidOperationException("Unknown Data Row Message");
            }
            return result;
        }
        #endregion

        #region Private Methods
        private async Task<IMessage> ProcessUpdateRow(Message message)
        {
            IMessage result = null;
            var info = message.GetContentAs<RowForm>();
            if (_process.HasPartialDatabase(info.DatabaseName))
            {
                var db = _process.GetPartialDatabase(info.DatabaseName);
                if (db.HasTable(info.TableName))
                {
                    var table = db.GetTable(info.TableName);
                    table.UpdateRow(info.Reference, info.RowValues);
                    var returnMessage = _process.Network.BuildMessage(message.Origin, null, MessageDataAction.Row.Update_Row_Information, MessageType.Data, message.RequestInformationId, MessageActionType.Table, null);
                    _process.Network.SendMessage(returnMessage);
                }
            }
            return result;
        }

        private IMessage ProcessDeleteRow(Message message)
        {
            IMessage result = new Message();
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

            return message;
        }
        private IMessage ProcessSaveRow(Message message)
        {
            IMessage result = new Message();
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

            return result;
        }
        #endregion

    }
}
