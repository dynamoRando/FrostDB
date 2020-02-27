using System;
using System.Collections.Generic;
using System.Text;

namespace FrostCommon.ConsoleMessages
{
    public class ContractInfo
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public string DatabaseName { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? ContractVersion { get; set; }
        public List<string> TableNames { get; set; }
        public string ContractDescription { get; set; }
        public LocationInfo Location { get; set; }
        public Guid? DatabaseId { get; set; }
        /// <summary>
        /// TableName, Permission Owner, Permissions
        /// </summary>
        public List<(string, string, List<string>)> SchemaData { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ContractInfo()
        {
            TableNames = new List<string>();
            SchemaData = new List<(string, string, List<string>)>();
            Location = new LocationInfo();
        }

        public ContractInfo(string databaseName, Guid? databaseId, LocationInfo location) : this()
        {
            this.DatabaseName = databaseName;
            this.DatabaseId = databaseId;
            this.Location = location;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}
