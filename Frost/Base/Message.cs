using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Message : IMessage
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public ILocation Destination { get; set; }
        public ILocation Origin { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion


    }
}
