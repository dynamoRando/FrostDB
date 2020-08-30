using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB
{
    /// <summary>
    /// Represents a data participant in a FrostDb system
    /// </summary>
    public class Participant2
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid? Id { get; set; }
        public Location2 Location { get; set; }
        #endregion

        #region Constructors
        public Participant2() { }
        public Participant2(Guid? id, Location2 location)
        {
            Id = id;
            Location = location;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
