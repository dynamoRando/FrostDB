using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DataManagerEventManager<TDatabase> : IDataManagerEventManager
        where TDatabase : IDatabase
    {
        #region Private Fields
        private DataManager<TDatabase> _dataManager;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataManagerEventManager(DataManager<TDatabase> manager)
        {
            _dataManager = manager;
        }
        #endregion

        #region Public Methods
        public void RegisterEvents()
        {
            RegisterTableCreatedEvents();
            RegisterRowAddedEvents();
            RegisterRowDeletedEvents();
            RegisterRowAccessedEvents();
            RegisterPendingPartcipantAddedEvents();
            RegisterPendingContractAddedEvents();
            RegisterMessageRecievedEvents();
            RegisterMessageSentEvents();
        }
        #endregion

        #region Private Methods
        private void RegisterMessageSentEvents()
        {
            EventManager.StartListening(EventName.Message.Message_Sent,
                new Action<IEventArgs>(HandleMessageSentEvent));
        }

        private void RegisterMessageRecievedEvents()
        {
            EventManager.StartListening(EventName.Message.Message_Recieved,
                new Action<IEventArgs>(HandleMessageRecievedEvent));
        }

        private void RegisterPendingContractAddedEvents()
        {
            EventManager.StartListening(EventName.Contract.Pending_Added,
            new Action<IEventArgs>(HandlePendingContractEvents));
        }
        private void RegisterPendingPartcipantAddedEvents()
        {
            EventManager.StartListening(EventName.Participant.Pending,
             new Action<IEventArgs>(HandleRegisterPendingPartcipantAddedEvents));
        }
        private void RegisterRowDeletedEvents()
        {
            EventManager.StartListening(EventName.Row.Deleted,
              new Action<IEventArgs>(HandleRowDeletedEvent));
        }
        private void RegisterTableCreatedEvents()
        {
            EventManager.StartListening(EventName.Table.Created,
                new Action<IEventArgs>(HandleCreatedTableEvent));
        }

        private void RegisterRowAddedEvents()
        {
            EventManager.StartListening(EventName.Row.Added,
               new Action<IEventArgs>(HandleRowAddedEvent));
        }

        private void RegisterRowAccessedEvents()
        {
            EventManager.StartListening(EventName.Row.Read,
               new Action<IEventArgs>(HandleRowAccessedEvent));
        }

        private void HandleMessageSentEvent(IEventArgs e)
        {
            if (e is MessageSentEventArgs)
            {
                var args = (MessageSentEventArgs)e;
                Console.WriteLine($"Message {args.Message.Id.ToString()} was sent for action {args.Message.Action}");
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
                        Console.WriteLine($"ACK: {args.Message.Origin} acknolweges message {args.Message.ReferenceMessageId}");
                    }
                }
            }
        }

        private void HandleRowDeletedEvent(IEventArgs e)
        {
            if (e is RowDeletedEventArgs)
            {
                var args = (RowDeletedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is TDatabase)
                {
                    _dataManager.SaveToDisk((TDatabase)db);
                }
            }
        }

        private void HandlePendingContractEvents(IEventArgs e)
        {
            if (e is PendingContractAddedEventArgs)
            {
                var args = (PendingContractAddedEventArgs)e;
                Console.WriteLine($"Pending Contract {args.Contract.DatabaseId} recieved");
            }
        }

        private void HandleRegisterPendingPartcipantAddedEvents(IEventArgs e)
        {
            if (e is ParticipantPendingEventArgs)
            {
                var args = (ParticipantPendingEventArgs)e;

                Console.WriteLine($"{args.DatabaseId} has pending participant at {args.Participant.Location.IpAddress}");
            }
        }

        private void HandleRowAccessedEvent(IEventArgs e)
        {
            if (e is RowAccessedEventArgs)
            {
                var args = (RowAccessedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is TDatabase)
                {
                    _dataManager.SaveToDisk((TDatabase)db);
                }
            }
        }

        private void HandleRowAddedEvent(IEventArgs e)
        {
            if (e is RowAddedEventArgs)
            {
                var args = (RowAddedEventArgs)e;

                IDatabase db = _dataManager.GetDatabase(args.DatabaseId);

                if (db is TDatabase)
                {
                    _dataManager.SaveToDisk((TDatabase)db);
                }
            }
        }

        private void HandleCreatedTableEvent(IEventArgs e)
        {
            if (e is TableCreatedEventArgs)
            {
                var args = (TableCreatedEventArgs)e;

                if (args.Database is TDatabase)
                {
                    args.Table.UpdateSchema();
                    args.Database.UpdateSchema();
                    _dataManager.SaveToDisk((TDatabase)args.Database);
                }
            }
        }
        #endregion
    }
}
