using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Linq;
using FrostDB.EventArgs;

namespace FrostDB.Base
{
    [Serializable]
    public class BaseDatabase : 
        IBaseDatabase, ISerializable, IFrostObjectGet, IDBObject
    {
        #region Private Fields
        private List<BaseTable> _tables;
        private string _name;
        private Guid? _id;
        private DbSchema _schema;
        private ParticipantManager _participantManager;
        private Contract _contract;
        #endregion

        #region Public Properties
        public string Name => _name;
        public List<BaseTable> Tables => _tables;
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
        public BaseDatabase()
        {
            _id = Guid.NewGuid();
            _tables = new List<BaseTable>();
        }
        public BaseDatabase(string name, BaseDataManager<IBaseDatabase> manager, Guid id,
            List<BaseTable> tables) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = new DbSchema(this);
        }

        public BaseDatabase(string name, BaseDataManager<IBaseDatabase> manager, Guid id,
            List<BaseTable> tables, DbSchema schema) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
        }

        public BaseDatabase(string name, BaseDataManager<IBaseDatabase> manager, Guid id,
            List<BaseTable> tables, DbSchema schema,
            List<Participant> participants) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, participants);
        }

        public BaseDatabase(string name, BaseDataManager<IBaseDatabase> manager, Guid id,
            List<BaseTable> tables, DbSchema schema,
            List<Participant> participants,
            Contract contract) : this(name)
        {
            _id = id;
            _tables = tables;
            _schema = schema;
            _participantManager = new ParticipantManager(this, participants);
            _contract = contract;
        }

        public BaseDatabase(string name) : this()
        {
            _name = name;
        }

        protected BaseDatabase(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
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

        public BaseTable GetTable(Guid? tableId)
        {
            return _tables.Where(t => t.Id == tableId).First();
        }

        public void AddTable(BaseTable table)
        {
            _tables.Add(table);
            EventManager.TriggerEvent(EventName.Table.Created,
              CreateTableCreatedEventArgs(table));
        }

        public BaseTable GetTable(string tableName)
        {
            return _tables.Where(t => t.Name == tableName).First();
        }

        public bool HasTable(string tableName)
        {
            return _tables.Any(t => t.Name == tableName);
        }

        #endregion

        #region Private Methods
        private TableCreatedEventArgs CreateTableCreatedEventArgs(BaseTable table)
        {
            var eventargs = new TableCreatedEventArgs();
            eventargs.Table = table;
            eventargs.Database = this;
            return eventargs;
        }
        #endregion

    }
}
