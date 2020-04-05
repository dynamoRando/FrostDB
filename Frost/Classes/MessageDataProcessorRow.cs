using FrostCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
				default:
					throw new InvalidOperationException("Unknown Data Row Message");
			}
		}
		#endregion

		#region Private Methods
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
