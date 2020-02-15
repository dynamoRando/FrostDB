using FrostCommon;
using FrostCommon.ConsoleMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public class MessageConsoleProcessorTable : IMessageConsoleProcessorObject
	{
		#region Private Fields
		private Process _process;
		private MessageBuilder _messageBuilder;
		#endregion

		#region Public Properties
		#endregion

		#region Protected Methods
		#endregion

		#region Events
		#endregion

		#region Constructors
		public MessageConsoleProcessorTable(Process process) 
		{
			_process = process;
			_messageBuilder = new MessageBuilder(_process);
		}
		#endregion

		#region Public Methods
		public void Process(Message message)
		{
			switch (message.Action)
			{
				case MessageConsoleAction.Table.Get_Table_Info:
					HandleGetTableInfo(message);
					break;
				case MessageConsoleAction.Table.Add_Column:
					HandleAddColumnMessage(message);
					break;
				case MessageConsoleAction.Table.Remove_Column:
					HandleRemoveColumnMessage(message);
					break;
			}
		}
		#endregion

		#region Private Methods
		private void HandleGetTableInfo(Message message)
		{
			var databaseTable = message.TwoGuidTuple;
			var db = _process.GetDatabase(databaseTable.Item1);
			var table = _process.GetTable(databaseTable.Item1, databaseTable.Item2);

			TableInfo info = new TableInfo();
			info.TableId = table.Id;
			info.TableName = table.Name;
			info.DatabaseName = db.Name;
			info.DatabaseId = table.DatabaseId;

			table.Columns.ForEach(c => info.Columns.Add((c.Name, c.DataType)));

			Type type = info.GetType();
			string messageContent = string.Empty;

			messageContent = JsonConvert.SerializeObject(info);
			_messageBuilder.SendResponse(message, messageContent, MessageConsoleAction.Table.Get_Table_Info_Response, type, MessageActionType.Table);
		}

		private void HandleAddColumnMessage(Message message)
		{
			var info = message.GetContentAs<ColumnInfo>();
			var db = _process.GetDatabase(info.DatabaseName);
			var table = db.GetTable(info.TableName);
			table.AddColumn(info.ColumnName, info.Type);
		}

		private void HandleRemoveColumnMessage(Message message)
		{
			var info = message.GetContentAs<ColumnInfo>();
			var db = _process.GetDatabase(info.DatabaseName);
			var table = db.GetTable(info.TableName);

			table.RemoveColumn(info.ColumnName);
		}
		#endregion

	}
}
