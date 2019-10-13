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
        private Type _type;
        #endregion

        #region Public Properties
        public Guid? Id => _id;
        public string Name => _name;
        public Type DataType => _type;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Column(string name, Type type)
        {
            _id = Guid.NewGuid();
            _name = name;
            _type = type;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

       
    }
}
