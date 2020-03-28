﻿using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class RowForm : IRowForm
    {
        #region Private Fields
        private Row _row;
        private Participant _participant;
        #endregion

        #region Public Properties
        public Row Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        public Participant Participant => _participant;
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public RowForm(Row row, Participant participant)
        {
            _row = row;
            _participant = participant;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
