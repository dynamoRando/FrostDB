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
		public IMessage Process(Message message)
		{
			IMessage result = null;
			switch (message.Action)
			{
				case MessageConsoleAction.Table.Get_Table_Info:
					result = HandleGetTableInfo(message);
					break;
				case MessageConsoleAction.Table.Add_Column:
					result = HandleAddColumnMessage(message);
					break;
				case MessageConsoleAction.Table.Remove_Column:
					result = HandleRemoveColumnMessage(message);
					break;
			}
			return result;
		}
		#endregion

		#region Private Methods
		private IMessage HandleGetTableInfo(Message message)
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
			return _messageBuilder.BuildMessage(message.Origin, messageContent, MessageConsoleAction.Table.Get_Table_Info_Response, type, message.Id, MessageActionType.Table);
		}

		private IMessage HandleAddColumnMessage(Message message)
		{
			IMessage result = new Message();
			var info = message.GetContentAs<ColumnInfo>();
			var db = _process.GetDatabase(info.DatabaseName);
			var table = db.GetTable(info.TableName);
			table.AddColumn(info.ColumnName, info.Type);
			return result;
		}

		private IMessage HandleRemoveColumnMessage(Message message)
		{
			IMessage result = new Message();
			var info = message.GetContentAs<ColumnInfo>();
			var db = _process.GetDatabase(info.DatabaseName);
			var table = db.GetTable(info.TableName);

			table.RemoveColumn(info.ColumnName);
			return result;
		}
		#endregion

	}
}
