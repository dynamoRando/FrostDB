using FrostDB.EventArgs;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
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
        }
        #endregion

        #region Private Methods
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

        private void HandleRowAddedEvent(IEventArgs e)
        {
            if (e is RowAddedEventArgs)
            {
                var args = (RowAddedEventArgs)e;

                if (args.Database is TDatabase)
                {
                    _dataManager.SaveToDisk((TDatabase)args.Database);
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
