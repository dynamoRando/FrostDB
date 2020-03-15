using FrostDB.Enum;
using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class ContractValidator : IContractValidator
    {
        #region Private Fields
        private Contract _contract;
        private Guid? _databaseId;
        #endregion

        #region Public Properties
        public bool ActionIsValidForParticipant(TableAction type, Participant participant)
        {
            // TO DO:  fix this
            return true;
        }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ContractValidator(Contract contract, Guid? databaseId)
        {
            _contract = contract;
            _databaseId = databaseId;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
