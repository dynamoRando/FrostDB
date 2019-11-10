using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class DataManagerEventManager<TDatabase> : IDataManagerEventManager
        where TDatabase : IBaseDatabase
    {
        #region Private Fields
        private BaseDataManager<TDatabase> _dataManager;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DataManagerEventManager(BaseDataManager<TDatabase> manager)
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
        }
        #endregion

        #region Private Methods
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

        private void HandleRowDeletedEvent(IEventArgs e)
        {
            if (e is RowDeletedEventArgs)
            {
                var args = (RowDeletedEventArgs)e;

                IBaseDatabase db = _dataManager.GetDatabase(args.DatabaseId);

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

                IBaseDatabase db = _dataManager.GetDatabase(args.DatabaseId);

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
                    _dataManager.SaveToDisk((TDatabase)args.Database);
                }
            }
        }
        #endregion
    }
}
