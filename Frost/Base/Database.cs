using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace FrostDB.Base
{
    [Serializable]
    public class Database : IDatabase
    {
        #region Private Fields
        private string _name;
        private List<ITable<Column, Row>> _tables;
        private IContract _contract;
        private DataManager<Database> _manager;
        #endregion

        #region Public Properties
        public Guid? Id { get; }
        public string Name { get { return _name; } }
        public List<ITable<Column, Row>> Tables { get { return _tables; } }
        public IContract Contract { get { return _contract; } }
        public DataManager<Database> Manager { get { return _manager; } }
        #endregion

        #region Events
        public event EventHandler<TableCreatedEventArgs> CreatedTable;
        #endregion

        #region Protected Methods
        protected virtual void OnTableCreated(TableCreatedEventArgs e)
        {
            if (CreatedTable != null)
            {
                Task.Run(() => CreatedTable.Invoke(this, e));
            }
        }
        #endregion

        #region Constructors
        protected Database(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid)serializationInfo.GetValue("Id", typeof(Guid));
            _name = (string)serializationInfo.GetValue("Name", typeof(string));
            _tables = (List<ITable<Column, Row>>)serializationInfo.GetValue
                ("Tables", typeof(List<ITable<Column, Row>>));
        }
        public Database(string name, DataManager<Database> manager)
        {
            Id = Guid.NewGuid();
            _name = name;
            _tables = new List<ITable<Column, Row>>();
            _manager = manager;
        }
        public Database(string name, DataManager<Database> manager, Guid id) : this(name, manager)
        {
            Id = id;
        }

        public Database(string name, DataManager<Database> manager, Guid id,
            List<ITable<Column, Row>> tables) : this(name, manager)
        {
            Id = id;
            _tables = tables;
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id.Value, typeof(Guid));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Tables", Tables, typeof(List<ITable<Column, Row>>));
        }
        public void AddTable(ITable<Column, Row> table)
        {
            _tables.Add(table);
            EventManager.TriggerEvent(EventName.Table.Created, 
                CreateTableCreatedEventArgs(table));
        }

        public bool HasTable(string tableName)
        {
            return this.Tables.Any(t => t.Name == tableName);
        }

        public ITable<Column, Row> GetTable(string tableName)
        {
            return this.Tables.
                Where(t => t.Name == tableName).First();
        }
        #endregion

        #region Private Methods
        private TableCreatedEventArgs CreateTableCreatedEventArgs(ITable<Column, Row> table)
        {
            var eventargs = new TableCreatedEventArgs();
            eventargs.Table = table;
            eventargs.Database = this;
            //OnTableCreated(eventargs);
            return eventargs;
        }
    }
    #endregion
}
