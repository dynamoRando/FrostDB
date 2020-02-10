using FrostDB.Interface;
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
        #endregion

        #region Public Properties
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
        public Database()
        {
            _id = Guid.NewGuid();
            _tables = new List<Table>();
            _participantManager = new ParticipantManager(this, new List<Participant>(), new List<Participant>());

            if (_schema is null)
            {
                _schema = new DbSchema(this);
            }

            if (_contract is null)
            {
                _contract = new Contract();
            }

        }
        public Database(string name, Guid id,
            List<Table> tables) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = new DbSchema(this);
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> acceptedParticipants) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, acceptedParticipants, new List<Participant>());
        }

        public Database(string name, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> acceptedParticipants,
            List<Participant> pendingParticipants,
            Contract contract) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, acceptedParticipants, pendingParticipants);
            _contract = contract;
        }

        public Database(string name) : this()
        {
            _name = name;
            _contract = new Contract(this);
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
                ProcessReference.Process.Id,
                (Location)Process.GetLocation());
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
                this.Contract.DatabaseLocation = ProcessReference.GetLocation();
                this.Contract.DatabaseSchema = this.Schema;
            }

            var contractMessage = new Message(
                destination: participant.Location, 
                origin: Process.GetLocation(), 
                messageContent: JsonExt.SeralizeContract(this.Contract), 
                messageAction: MessageDataAction.Contract.Save_Pending_Contract,
                messageType: MessageType.Data
                );

            //TO DO: Should this wait if the send is successful or not before adding participant?
            NetworkReference.SendMessage(contractMessage);
            _participantManager.AddPendingParticipant(participant);
        }

        public void AddParticipant(Participant participant)
        {
            _participantManager.AddParticipant(participant);
        }

        public void UpdateSchema()
        {
            _schema = new DbSchema(this);
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
                EventManager.TriggerEvent(EventName.Table.Created,
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
            return AcceptedParticipants.Any(participant => !participant.Location.IsLocal());
        }
        public void RemoveTable(string tableName)
        {
            var table = this.GetTable(tableName);
            _tables.Remove(table);
            EventManager.TriggerEvent(EventName.Table.Dropped,
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

    }
}
