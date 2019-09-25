﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Process : IProcess
    {
        #region Private Fields
        private IConfiguration _configuration;
        private ICommService _commService;
        private Guid _id;
        private string _name;
        private IDatabaseManager _databaseManager;
        #endregion

        #region Public Properties
        public List<IDatabase> Databases { get { return _databaseManager.Databases; } }
        public Guid Id { get { return _id; } }
        public string Name { get { return _name; } }
        public IConfiguration Configuration { get { return _configuration; } }
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Process()
        {
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
