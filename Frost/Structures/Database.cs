﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using FrostDB.EventArgs;
using System.Threading.Tasks;
using FrostCommon;
using FrostDB.Extensions;

namespace FrostDB
{
    [Serializable]
    public class Database :
        IDatabase, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private List<Table> _tables;
        private string _name;
        private Guid? _id;
        private DbSchema _schema;
        private ParticipantManager _participantManager;
        private Contract _contract;
        private Process _process;

        #endregion

        #region Public Properties
        public List<Table2> Tables2 { get; set; }
        public string Name => _name;
        public List<Table> Tables => _tables;
        public Guid? Id => _id;
        public DbSchema Schema => _schema;
        public List<Participant> AcceptedParticipants => _participantManager.AcceptedParticipants;
        public List<Participant> PendingParticipants => _participantManager.PendingParticipants;
        public Contract Contract => _contract;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database(Process process, IDbFill fill, DbStorage dbStorage)
        {
            throw new NotImplementedException();
        }

        public Database(Process process)
        {
            Tables2 = new List<Table2>();
            _process = process;
            _id = Guid.NewGuid();
            _tables = new List<Table>();
            _participantManager = new ParticipantManager(this, new List<Participant>(), new List<Participant>(), _process);

            if (_schema is null)
            {
                _schema = new DbSchema(this, _process);
            }

            if (_contract is null)
            {
                _contract = new Contract(_process);
            }

            SetProcessForParticipants();
        }
        public Database(string name, Guid id,
            List<Table> tables, Process process) : this(name, process)
        {
            _id = id;
            _tables = tables;
            _schema = new DbSchema(this, _process);
            SetProcessForParticipants();
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema, Process process) : this(name, process)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            SetProcessForParticipants();
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> acceptedParticipants, Process process) : this(name, process)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, acceptedParticipants, new List<Participant>(), _process);
            SetProcessForParticipants();
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> acceptedParticipants,
            List<Participant> pendingParticipants,
            Contract contract, Process process) : this(name, process)
        {
            _id = id;
            _tables = tables;
            _schema = schema;

            if (acceptedParticipants is null)
            {
                acceptedParticipants = new List<Participant>();
            }

            if (pendingParticipants is null)
            {
                pendingParticipants = new List<Participant>();
            }

            if (contract is null)
            {
                contract = new Contract(_process, this);
            }

            _participantManager = new ParticipantManager(this, acceptedParticipants, pendingParticipants, _process);
            _contract = contract;

            _process = process;
            SetProcessForParticipants();
        }

        public Database(string name, Process process) : this(process)
        {
            _name = name;
            _contract = new Contract(_process, this);
            SetProcessForParticipants();
        }

        protected Database(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
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
            this.UpdateSchema();

            this.Contract.DatabaseName = this.Name;
            this.Contract.DatabaseId = this.Id;
            this.Contract.DatabaseLocation = _process.GetLocation();
            this.Contract.DatabaseSchema = this.Schema;

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
            _schema = new DbSchema(this, _process);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public Table GetTable(Guid? tableId)
        {
            return _tables.Where(t => t.Id == tableId).First();
        }

        public void AddTable(Table2 table)
        {
            throw new NotImplementedException();
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
            return _tables.Where(t => t.Name.ToUpper() == tableName.ToUpper()).First();
        }

        public bool HasTable(string tableName)
        {
            return _tables.Any(t => t.Name.ToUpper().Equals(tableName.ToUpper()));
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
        private void SetProcessForParticipants()
        {
            foreach (var p in AcceptedParticipants)
            {
                p.SetProcess(_process);
            }
        }
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

    }
}
