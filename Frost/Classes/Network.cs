﻿using System;
using FrostCommon;
using FrostCommon.Net;
using FrostDB.EventArgs;

namespace FrostDB
{
    public class Network
    {
        #region Private Fields
        MessageDataProcessor _messageDataProcessor;
        MessageConsoleProcessor _messageConsoleProcessor;
        Server _dataServer;
        Server _consoleServer;
        #endregion

        #region Public Properties
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Network()
        {
            _messageConsoleProcessor = new MessageConsoleProcessor();
            _messageDataProcessor = new MessageDataProcessor();
            _dataServer = new Server();
            _consoleServer = new Server();
        }
        #endregion

        #region Public Methods
        public void StartDataServer()
        {
            _dataServer.Start(Process.Configuration.DataServerPort, Process.Configuration.Address, _messageDataProcessor);
        }

        public void StopDataServer()
        {
            _dataServer.Stop();
        }

        public void StartConsoleServer()
        {
            _consoleServer.Start(Process.Configuration.ConsoleServerPort, Process.Configuration.Address, _messageConsoleProcessor);
        }

        public void StopConsoleServer()
        {
            _consoleServer.Stop();
        }
        public void SendMessage(Message message)
        {
            Client.Send(message);
            EventManager.TriggerEvent(EventName.Message.Message_Sent, CreateMessageSentEventArgs(message));
        }

        #endregion

        #region Private Methods
        private static MessageSentEventArgs CreateMessageSentEventArgs(Message message)
        {
            string data = Json.SeralizeMessage(message);
            return new MessageSentEventArgs
            {
                Message = message,
                MessageLength = data.Length,
                StringMessage = data
            };
        }
        #endregion

    }
}