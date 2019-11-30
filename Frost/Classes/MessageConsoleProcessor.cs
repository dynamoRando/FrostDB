﻿using System;
using FrostDB.Interface;
using FrostCommon;
using FrostDB.Extensions;

namespace FrostDB
{
    public class MessageConsoleProcessor : BaseMessageProcessor
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
        public MessageConsoleProcessor() : base()
        {

        }
        #endregion

        #region Public Methods
        public override void Process(IMessage message)
        {
            base.HandleProcessMessage(message);
            // process messages from the console
        }
        #endregion

        #region Private Methods
        #endregion


    }
}
