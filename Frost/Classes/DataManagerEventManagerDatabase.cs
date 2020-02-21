using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.EventArgs;
using FrostDB.Interface;

namespace FrostDB
{
    public class DataManagerEventManagerDatabase : IDataManagerEventManager
    {
        #region Private Fields
        private DatabaseManager _dataManager;
        private Process _process;
        #endregion

        #region Public Properties
        public DatabaseManager Manager
        {
            get
            {
                return _dataManager;
            }
            set
            {
                _dataManager = value;
            }
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataManagerEventManagerDatabase(Process process) 
        {
            _process = process;
        }
        public DataManagerEventManagerDatabase(DatabaseManager manager)
        {
            _dataManager = manager;
        }
        #endregion

        #region Public Methods
        public void RegisterEvents()
        {
            RegisterTableCreatedEvents();
            RegisterTableDroppedEvents();
            RegisterRowAddedEvents();
            RegisterRowDeletedEvents();
            RegisterRowAccessedEvents();
            RegisterPendingPartcipantAddedEvents();
            RegisterPendingContractAddedEvents();
            RegisterMessageRecievedEvents();
            RegisterMessageSentEvents();
            RegisterColumnAddedEvents();
            RegisterColumnRemovedEvents();
            RegisterContractUpdatedEvents();
            RegisterParticipantAddedEvents();
        }
        #endregion

        #region Private Methods
        private void RegisterParticipantAddedEvents()
        {
            _process.EventManager.StartListening(EventName.Participant.Added,
                new Action<IEventArgs>(HandleParticipantAddedEvent));

        }

        private void RegisterContractUpdatedEvents()
        {
            _process.EventManager.StartListening(EventName.Contract.Contract_Updated, new Action<IEventArgs>(HandleContractUpdatedEvent));
        }

        private void RegisterColumnRemovedEvents()
        {
            _process.EventManager.StartListening(EventName.Columm.Deleted, new Action<IEventArgs>(HandleColumnDeletedEvent));
        }

        private void RegisterTableDroppedEvents()
        {
            _process.EventManager.StartListening(EventName.Table.Dropped, new Action<IEventArgs>(HandleTableDroppedEvent));
        }
        private void RegisterMessageSentEvents()
        {
            _process.EventManager.StartListening(EventName.Message.Message_Sent,
                new Action<IEventArgs>(HandleMessageSentEvent));
        }

        private void RegisterMessageRecievedEvents()
        {
            _process.EventManager.StartListening(EventName.Message.Message_Recieved,
                new Action<IEventArgs>(HandleMessageRecievedEvent));
        }

        private void RegisterPendingContractAddedEvents()
        {
            _process.EventManager.StartListening(EventName.Contract.Pending_Added,
            new Action<IEventArgs>(HandlePendingContractEvents));
        }
        private void RegisterPendingPartcipantAddedEvents()
        {
            _process.EventManager.StartListening(EventName.Participant.Pending,
             new Action<IEventArgs>(HandleRegisterPendingPartcipantAddedEvents));
        }
        private void RegisterRowDeletedEvents()
        {
            _process.EventManager.StartListening(EventName.Row.Deleted,
              new Action<IEventArgs>(HandleRowDeletedEvent));
        }
        private void RegisterTableCreatedEvents()
        {
            _process.EventManager.StartListening(EventName.Table.Created,
                new Action<IEventArgs>(HandleCreatedTableEvent));
        }

        private void RegisterRowAddedEvents()
        {
            _process.EventManager.StartListening(EventName.Row.Added,
               new Action<IEventArgs>(HandleRowAddedEvent));
        }

        private void RegisterRowAccessedEvents()
        {
            _process.EventManager.StartListening(EventName.Row.Read,
               new Action<IEventArgs>(HandleRowAccessedEvent));
        }

        private void RegisterColumnAddedEvents()
        {
            _process.EventManager.StartListening(EventName.Columm.Added, new Action<IEventArgs>(HandleColumnAddedEvent));
        }

        private void HandleParticipantAddedEvent(IEventArgs e)
        {
            if (e is ParticipantAddedEventArgs)
            {
                var args = (ParticipantAddedEventArgs)e;
                if (_process.HasDatabase(args.DatabaseId))
                {
                    var db = _process.GetDatabase(args.DatabaseId);
                    if (db is Database)
                    {
                        _dataManager.SaveToDisk((Database)db);
                    }
                }
            }
        }

        private void HandleMessageSentEvent(IEventArgs e)
        {
            if (e is MessageSentEventArgs)
            {
                var args = (MessageSentEventArgs)e;
                Console.WriteLine($"Message {args.Message.Id.ToString()} was sent for action {args.Message.Action}");
            }
        }

        private void HandleContractUpdatedEvent(IEventArgs e)
        {
            if (e is ContractUpdatedEventArgs)
            {
                var args = (ContractUpdatedEventArgs)e;
                if (args.Database is Database)
                {
                    _dataManager.SaveToDisk((Database)args.Database);
                }
            }
        }

        private void HandleMessageRecievedEvent(IEventArgs e)
        {
            if (e is MessageRecievedEventArgs)
            {
                var args = (MessageRecievedEventArgs)e;
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    args.MessageLength, args.StringMessage);

                if (args.Message.ReferenceMessageId.HasValue)
                {
                    if (args.Message.ReferenceMessageId.Value != Guid.Empty)
                    {
                        Console.WriteLine($"ACK: {args.Message.Origin.IpAddress} acknolweges message {args.Message.ReferenceMessageId}");
                    }
                }
            }
        }

        private void HandleColumnDeletedEvent(IEventArgs e)
        {
            if (e is ColumnDeletedEventArgs)
            {
                var args = (ColumnDeletedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseName);
                if (db is Database)
                {
                    db.GetTable(args.TableName).UpdateSchema();
                    db.UpdateSchema();
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandleColumnAddedEvent(IEventArgs e)
        {
            if (e is ColumnAddedEventArgs)
            {
                var args = (ColumnAddedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseName);
                if (db is Database)
                {
                    db.GetTable(args.TableName).UpdateSchema();
                    db.UpdateSchema();
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandleRowDeletedEvent(IEventArgs e)
        {
            if (e is RowDeletedEventArgs)
            {
                var args = (RowDeletedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is Database)
                {
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandlePendingContractEvents(IEventArgs e)
        {
            if (e is PendingContractAddedEventArgs)
            {
                var args = (PendingContractAddedEventArgs)e;
                Console.WriteLine($"Pending Contract {args.Contract.DatabaseId} recieved");

                // TO DO: Check if db exists first, if not, then this is a new partial db to be created
                if (_process.HasDatabase(args.Contract.DatabaseId))
                {
                    var db = _process.GetDatabase(args.Contract.DatabaseId);
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandleRegisterPendingPartcipantAddedEvents(IEventArgs e)
        {
            if (e is ParticipantPendingEventArgs)
            {
                var args = (ParticipantPendingEventArgs)e;
                if (_process.HasDatabase(args.DatabaseId))
                {
                    var db = _process.GetDatabase(args.DatabaseId);
                    _dataManager.SaveToDisk((Database)db);
                }
                Console.WriteLine($"{args.DatabaseId} has pending participant at {args.Participant.Location.IpAddress}");
            }
        }

        private void HandleRowAccessedEvent(IEventArgs e)
        {
            if (e is RowAccessedEventArgs)
            {
                var args = (RowAccessedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is Database)
                {
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandleRowAddedEvent(IEventArgs e)
        {
            if (e is RowAddedEventArgs)
            {
                var args = (RowAddedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is Database)
                {
                    _dataManager.SaveToDisk((Database)db);
                }
            }
        }

        private void HandleTableDroppedEvent(IEventArgs e)
        {
            if (e is TableDroppedEventArgs)
            {
                var args = (TableDroppedEventArgs)e;
                if (args.Database is Database)
                {
                    args.Database.UpdateSchema();
                    _dataManager.SaveToDisk((Database)args.Database);
                }
            }
        }

        private void HandleCreatedTableEvent(IEventArgs e)
        {
            if (e is TableCreatedEventArgs)
            {
                var args = (TableCreatedEventArgs)e;

                if (args.Database is Database)
                {
                    args.Table.UpdateSchema();
                    args.Database.UpdateSchema();
                    _dataManager.SaveToDisk((Database)args.Database);
                }
            }
        }
        #endregion
    }
}
