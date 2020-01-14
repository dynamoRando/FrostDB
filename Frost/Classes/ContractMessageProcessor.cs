using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB
{
    public class ContractMessageProcessor 
    {
        #region Private Fields
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static void Process(Message message)
        {
            switch (message.Action)
            {
                case MessageDataAction.Contract.Save_Pending_Contract:
                    SavePendingContract(message);
                    break;
                default:
                    throw new InvalidOperationException("Unknown Contract Message");
            }
        }
        #endregion

        #region Private Methods
        private static void SavePendingContract(Message message)
        {
            Contract contract = null;
            if (JsonExt.TryParse(message.Content, out contract))
            {
                Console.WriteLine("Saving pending contract");
                ProcessReference.Process.AddPendingContract(contract);
            }
        }
        #endregion


    }
}
