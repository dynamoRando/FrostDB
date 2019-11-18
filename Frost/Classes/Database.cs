using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;
using FrostDB.EventArgs;

namespace FrostDB
{
    [Serializable]
    public class Database : 
        IBaseDatabase, ISerializable, IFrostObjectGet, IDBObject
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
        public List<Participant> Participants => _participantManager.Participants;
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
        }
        public Database(string name, DataManager<IBaseDatabase> manager, Guid id,
            List<Table> tables) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = new DbSchema(this);
        }

        public Database(string name, DataManager<IBaseDatabase> manager, Guid id,
            List<Table> tables, DbSchema schema) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
        }

        public Database(string name, DataManager<IBaseDatabase> manager, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> participants) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, participants);
        }

        public Database(string name, DataManager<IBaseDatabase> manager, Guid id,
            List<Table> tables, DbSchema schema,
            List<Participant> participants,
            Contract contract) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, participants);
            _contract = contract;
        }

        public Database(string name) : this()
        {
            _name = name;
        }

        protected Database(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public Participant GetProcessParticipant()
        {
            return new Participant(
                ProcessReference.Process.Id,
                (Location)Process.GetLocation());
        }
        public void SendParticipantRegistration(Location location)
        {
            throw new NotImplementedException();
        }
        public void AddNewParticipant(Participant participant)
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
            _tables.Add(table);
            EventManager.TriggerEvent(EventName.Table.Created,
              CreateTableCreatedEventArgs(table));
        }

        public Table GetTable(string tableName)
        {
            return _tables.Where(t => t.Name == tableName).First();
        }

        public bool HasTable(string tableName)
        {
            return _tables.Any(t => t.Name == tableName);
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
        #endregion

    }
}
