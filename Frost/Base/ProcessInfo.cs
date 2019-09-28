using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using FrostDB.Enum;
using FrostDB.Interface;

namespace FrostDB.Base
{
    public class ProcessInfo : IProcessInfo
    {
        #region Private Fields
        private OSPlatform _operatingSystem;
        private ProcessType _processType;
        #endregion

        #region Public Properties
        public OSPlatform OS => _operatingSystem;
        public ProcessType Type => _processType;
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ProcessInfo(OSPlatform os, ProcessType type)
        {
            _operatingSystem = os;
            _processType = type;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
            
    }
}
