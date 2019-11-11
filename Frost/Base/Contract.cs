using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Base
{
    public class Contract : IContract
    {

        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public Location DatabaseLocation { get; set; }
        public DbSchema DatabaseSchema { get; set; }
        public string ContractDescription { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ContractVersion { get; set; }
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
