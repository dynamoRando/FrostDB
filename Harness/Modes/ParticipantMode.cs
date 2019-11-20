using FrostDB;
using Harness.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class ParticipantMode : BaseMode
    {

        #region Private Fields
        private bool _stayInMode;
        #endregion

        #region Public Properties
        public Database Database { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ParticipantMode(App app) : base(app) { }
        public ParticipantMode(App app, Database database) : base(app) { Database = database; }
        #endregion

        #region Public Methods

        public override void Prompt()
        {
            _stayInMode = true;

            do
            {
                var result = Prompt("Would you like to (a) - add pending partipant, (ad) - add pending default participant, or (em) - exit?");

                switch (result)
                {
                    case "ad":
                        AddPendingDefaultParticipant();
                        break;
                    case "a":
                        AddPendingParticipant();
                        break;
                }
            } while (_stayInMode);
        }

        #endregion

        #region Private Methods
        private void AddPendingDefaultParticipant()
        {
            var result = Prompt($"Will add default participant 192.168.1.14? (y) or will exit");
            if (result == "y")
            {
                var location = new Location(Guid.NewGuid(), "192.168.1.14", 516, "FrostUbuntu");
                var participant = new Participant(location);
                participant.Contract = Database.Contract;
                var db = ProcessReference.GetDatabase(Database.Id);
                
            }
        }

        private void AddPendingParticipant()
        {
            var ipAddress = Prompt($"Enter IP Address");
            var portNumber = Prompt("Enter PortNumber");
            var result = Prompt($"Will send particpant a contract at {ipAddress} on port {portNumber} is this correct? (y) or exit");
            if (result == "y")
            {
                var location = new Location(Guid.NewGuid(), ipAddress, Convert.ToInt32(portNumber), "FrostTest");
                var participant = new Participant(location);
                participant.Contract = Database.Contract;
                var db = ProcessReference.GetDatabase(Database.Id);
                db.AddParticipant(participant);
            }
        }

        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write($"[Mode -> Database: ({Database.Name})] [(em) to exit mode] ==>");

            var _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                App.Write("Quitting...");
                App.Quit();
            }

            if (_consoleLine == "em")
            {
                App.Write("Quitting ParticipantMode...");
                _stayInMode = false;
            }

            return _consoleLine;
        }
        #endregion
    }
}
