using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using FrostCommon;

namespace FrostDB
{
    /// <summary>
    /// Holds list of participants for this db that have accepted/pending acceptance of the db contract
    /// </summary>
    public class ParticipantFile : IStorageFile
    {
        #region Private Fields
        private string _participantFileExtension;
        private string _participantFileFolder;
        private string _databaseName;
        private List<Participant2> _accepted;
        private List<Participant2> _pending;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a file that holds the participants for a db that are pending or have accepted
        /// </summary>
        /// <param name="extension">The file extension for the participant file</param>
        /// <param name="folder">The folder which participant files are held</param>
        /// <param name="databaseName">The name of the database for this participant file</param>
        public ParticipantFile(string extension, string folder, string databaseName)
        {
            _participantFileExtension = extension;
            _participantFileFolder = folder;
            _databaseName = databaseName;
            _accepted = new List<Participant2>();
            _pending = new List<Participant2>();
            
            if (DoesFileExist())
            {
                LoadFile();
            }
            else
            {
                CreateFile();
            }

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a list of participants that have accepted the contract for this database
        /// </summary>
        /// <returns>The list of accepted participants</returns>
        public List<Participant2> GetAcceptedParticipants()
        {
            return _accepted;
        }

        /// <summary>
        /// Returns a list of pending participants where a contract has been sent but not accecpted/rejected
        /// </summary>
        /// <returns>The list of pending participants</returns>
        public List<Participant2> GetPendingParticipants()
        {
            return _pending;
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a new Schema file for this database
        /// </summary>
        private void CreateFile()
        {
            SetVersionNumberIfBlank();

            using (var file = new StreamWriter(FileName()))
            {
                file.WriteLine("version " + VersionNumber.ToString());
            }
        }

        /// <summary>
        /// Checks the version number of this file. If it is not set, it will default to Version 1
        /// </summary>
        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.PARTICIPANT_FILE_VERSION_1;
            }
        }

        /// <summary>
        /// Returns the filename for the participant file for this database
        /// </summary>
        /// <returns>The filename</returns>
        private string FileName()
        {
            return Path.Combine(_participantFileFolder, _databaseName + "." + _participantFileExtension);
        }

        /// <summary>
        /// Determines if the participant file exists on disk
        /// </summary>
        /// <returns>True if participant file exists for this database, otherwise false</returns>
        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        /// <summary>
        /// Loads the participant file from disk
        /// </summary>
        private void LoadFile()
        {
            var lines = File.ReadAllLines(FileName()).ToList();
            lines.ForEach(l => ParseLine(l));
        }

        /// <summary>
        /// Parses a line from the participant file
        /// </summary>
        /// <param name="line">A line from the file</param>
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

        /// <summary>
        /// Parses a line from disk for participant information
        /// </summary>
        /// <param name="line">The line from the participant file</param>
        private void ParseParticipant(string line)
        {
            var items = line.Split(" ");
            // participant participantId ipaddress:portNumber true/false (accepted)
            var ipaddressInfo = items[2].Split(":");
            var ipaddress = ipaddressInfo[0];
            var portNumber = ipaddressInfo[1];
            var isAccepted = Convert.ToBoolean(items[3]);

            Guid id = Guid.Parse(items[1]);
            Location2 location = new Location2(ipaddress, Convert.ToInt32(portNumber));
            var particpant = new Participant2(id, location);

            if (isAccepted)
            {
                _accepted.Add(particpant);
            }
            else
            {
                _pending.Add(particpant);
            }
        }

        /// <summary>
        /// Parses the version information from a line in the participant file
        /// </summary>
        /// <param name="line">The line to parse version information from</param>
        private void ParseVersion(string line)
        {
            // version versionNumber
            var items = line.Split(" ");
            VersionNumber = Convert.ToInt32(items[1]);
        }
        #endregion
    }
}
