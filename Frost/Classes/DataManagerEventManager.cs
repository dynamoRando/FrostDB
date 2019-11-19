﻿using FrostDB.EventArgs;
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

        private void RegisterRowAccessedEvents()
        {
            EventManager.StartListening(EventName.Row.Read,
               new Action<IEventArgs>(HandleRowAccessedEvent));
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