using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Interface;

namespace FrostDB
{
    public class DataManagerEventManagerPartialDatabase : IDataManagerEventManager
    {
		#region Private Fields
		private PartialDatabaseManager _dataManager;
		#endregion

		#region Public Properties
		public PartialDatabaseManager Manager
		{
			get
			{
				return _dataManager;
			}
			set
			{
				_dataManager = value;
			}
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Events
		#endregion

		#region Constructors
		#endregion

		#region Public Methods
		public void RegisterEvents()
		{
			
		}
		#endregion

		#region Private Methods
		#endregion


	}
}
