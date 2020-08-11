using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    public class ParticipantFile : IStorageFile
    {
        #region Private Fields
        private string _participantFileExtension;
        private string _participantFileFolder;
        private string _databaseName;
        private List<Participant> _accepted;
        private List<Participant> _pending;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ParticipantFile(string extension, string folder, string databaseName)
        {
            _participantFileExtension = extension;
            _participantFileFolder = folder;
            _databaseName = databaseName;
            _accepted = new List<Participant>();
            _pending = new List<Participant>();
        }
        #endregion

        #region Public Methods
        public List<Participant> GetAcceptedParticipants()
        {
            throw new NotImplementedException();
        }

        public List<Participant> GetPendingParticipants()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void LoadFile()
        {
            var file = Path.Combine(_participantFileFolder, _databaseName + "." + _participantFileExtension);
            var lines = File.ReadAllLines(file).ToList();
            lines.ForEach(l => ParseLine(l));
        }

        private void ParseLine(string line)
        {
            // version versionNumber
            if (line.StartsWith("version"))
            {
                ParseVersion(line);
            }

            // participant participantId ipaddress:portNumber true/false (accepted)
            if (line.StartsWith("participant"))
            {
                ParseParticipant(line);
            }
        }

        private void ParseParticipant(string line)
        {
            throw new NotImplementedException();
        }

        private void ParseVersion(string line)
        {
            // version versionNumber
            var items = line.Split(" ");
            VersionNumber = Convert.ToInt32(items[1]);
        }
        #endregion


    }
}
