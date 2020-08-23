using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Database2
    {
        #region Private Fields
        private Process _process;
        private string _name;
        private List<Table2> _tables;
        #endregion

        #region Public Properties
        public string Name => _name;
        public List<Table2> Tables => _tables;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Database2(Process process, string name)
        {
            _process = _process;
            _name = name;
        }

        public Database2(Process process, DbFill fill)
        {
            _process = process;
            _name = fill.Schema2.DatabaseName;
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
