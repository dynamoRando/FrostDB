using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using FrostDB.Enum;
using FrostDB.Interface;

namespace FrostDB
{
    public class ProcessInfo : IProcessInfo
    {
        #region Private Fields
        private OSPlatform _operatingSystem;
        #endregion

        #region Public Properties
        public OSPlatform OS => _operatingSystem;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ProcessInfo(OSPlatform os)
        {
            _operatingSystem = os;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
            
    }
}
