﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using FrostDB.Interface;
using System.Linq;
using FrostCommon;
using FrostDB.EventArgs;
using FrostDB.Extensions;
using FrostCommon.ConsoleMessages;

namespace FrostDB
{
    [Serializable]
    public class PartialDatabase : IDatabase
    {
        #region Private Fields
        private List<Table> _tables;
        private string _name;
        private Guid? _id;
        private DbSchema _schema;
        private ParticipantManager _participantManager;
        private Contract _contract;
        private Process _process;
        private DatabaseReference _referenceDatabase;

        #endregion

        #region Public Properties
        public string Name => _name;
        public List<Table> Tables => _tables;
        public Guid? Id => _id;
        public DbSchema Schema => _schema;
        public List<Participant> AcceptedParticipants => _participantManager.AcceptedParticipants;
        public List<Participant> PendingParticipants => _participantManager.PendingParticipants;
        public Contract Contract => _contract;
        public DatabaseReference ReferenceDatabase => _referenceDatabase;
        #endregion


        #region Constructors
        public PartialDatabase(Process process) 
        {
            _id = Guid.NewGuid();
            _process = process;
            _tables = new List<Table>();

            if (_schema is null)
            {
                _schema = new DbSchema(this, _process);
            }

            if (_contract is null)
            {
                _contract = new Contract(_process);
            }
        }
        public PartialDatabase(string name, Process process) : this(process)
        {
            _name = name;
            _process = process;
        }

        public PartialDatabase(string name, Process process, DatabaseReference reference) : this(process)
        {
            _name = name;
            _process = process;
            _referenceDatabase = reference;
        }

        public PartialDatabase(string name, Guid id,
           List<Table> tables, Process process)  : this(process)
        {
            _name = name;
            _process = process;
            _tables = tables;
            _id = id;
        }
        #endregion

        #region Public Methods
        public void AddTable(Table2 table)
        {
            throw new NotImplementedException();
        }

        public void AddTable(TableSchemaInfo schema)
        {
            if (!HasTable(schema.TableName))
            {
                var columns = new List<Column>();
                schema.Columns.ForEach(c => columns.Add(new Column(c.ColumnName, Type.GetType(c.DataType))));

                var table = new Table(schema.TableName, columns, _id, _process);
                Tables.Add(table);
            }
        }
        public string GetTableName(Guid? tableId)
        {
            return Tables.Where(t => t.Id == tableId).First().Name;
        }
        public Guid? GetTableId(string tableName)
        {
            return Tables.Where(t => t.Name == tableName).First().Id;
        }
        public bool HasTable(Guid? tableId)
        {
            return Tables.Any(t => t.Id == tableId);
        }
        public Participant GetProcessParticipant()
        {
            return new Participant(
                _process.Id,
                (Location)_process.GetLocation());
        }

        public Participant GetParticipant(Guid? participantId)
        {
            return AcceptedParticipants.Where(p => p.Id == participantId).First();
        }

        public void RemovePendingParticipant(Participant participant)
        {
            var p = PendingParticipants.Where(p => p.Location.IpAddress == participant.Location.IpAddress && p.Location.PortNumber == participant.Location.PortNumber).FirstOrDefault();
            PendingParticipants.Remove(p);
        }

        public Participant GetParticipant(string ipAddress, int portNumber)
        {
            return AcceptedParticipants.Where(p => p.Location.IpAddress == ipAddress && p.Location.PortNumber == portNumber).First();
        }

        public Participant GetPendingParticipant(string ipAddress, int portNumber)
        {
            return PendingParticipants.Where(p => p.Location.IpAddress == ipAddress && p.Location.PortNumber == portNumber).First();
        }

        public bool HasParticipant(Guid? participantId)
        {
            return AcceptedParticipants.Any(p => p.Id == participantId);
        }

        public void AddPendingParticipant(Participant participant)
        {
            if (string.IsNullOrEmpty(this.Contract.DatabaseName))
            {
                this.UpdateSchema();

                this.Contract.DatabaseName = this.Name;
                this.Contract.DatabaseId = this.Id;
                this.Contract.DatabaseLocation = _process.GetLocation();
                this.Contract.DatabaseSchema = this.Schema;
            }

            var contractMessage = new Message(
                destination: participant.Location,
                origin: _process.GetLocation(),
                messageContent: JsonExt.SeralizeContract(this.Contract),
                messageAction: MessageDataAction.Contract.Save_Pending_Contract,
                messageType: MessageType.Data
                );

            //TO DO: Should this wait if the send is successful or not before adding participant?
            _process.Network.SendMessage(contractMessage);
            _participantManager.AddPendingParticipant(participant);
        }

        public void AddParticipant(Participant participant)
        {
            _participantManager.AddParticipant(participant);
        }

        public void UpdateSchema()
        {
            throw new NotImplementedException();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public Table GetTable(Guid? tableId)
        {
            return _tables.Where(t => t.Id == tableId).First();
        }

        public void AddTable(Table table)
        {
            if (!HasTable(table.Name))
            {
                _tables.Add(table);
                _process.EventManager.TriggerEvent(EventName.Table.Created,
                  CreateTableCreatedEventArgs(table));
            }
        }

        public Table GetTable(string tableName)
        {
            return _tables.Where(t => t.Name == tableName).First();
        }

        public bool HasTable(string tableName)
        {
            return _tables.Any(t => t.Name == tableName);
        }

        public bool IsCooperative()
        {
            return AcceptedParticipants.Any(participant => !participant.Location.IsLocal(_process));
        }
        public void RemoveTable(string tableName)
        {
            var table = this.GetTable(tableName);
            _tables.Remove(table);
            _process.EventManager.TriggerEvent(EventName.Table.Dropped,
                TableDroppedEventArgs(table));
        }

        public void RemoveTable(Guid? tableId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private TableCreatedEventArgs CreateTableCreatedEventArgs(Table table)
        {
            var eventargs = new TableCreatedEventArgs();
            eventargs.Table = table;
            eventargs.Database = this;
            return eventargs;
        }
        private TableDroppedEventArgs TableDroppedEventArgs(Table table)
        {
            var eventargs = new TableDroppedEventArgs();
            eventargs.Table = table;
            eventargs.Database = this;
            return eventargs;
        }
        #endregion

        #region Protected Methods

        protected PartialDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext, Process process) 
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
