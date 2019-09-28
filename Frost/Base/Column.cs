using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Column : IColumn
    {
        #region Private Fields
        private Guid? _id;
        private string _name;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public string Name => _name;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Column(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

       
    }
}
