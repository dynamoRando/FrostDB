using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    public class SchemaFile : IStorageFile
    {
        #region Private Fields
        private string _schemaFileExtension;
        private string _schemaFileFolder;
        private string _databaseName;
        private string _fileText;
        #endregion

        #region Public Properties
        public int VersionNumber { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public SchemaFile(string schemaFileFolder, string fileExtension, string databaseName)
        {
            _schemaFileExtension = schemaFileFolder;
            _schemaFileExtension = fileExtension;
            _databaseName = databaseName;
        }
        #endregion

        #region Public Methods
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
        public void Load()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void LoadFile()
        {
            var file = Path.Combine(_schemaFileFolder, _databaseName + "." + _schemaFileExtension);
            var lines = File.ReadAllLines(file).ToList();
            throw new NotImplementedException();
        }

        private void ParseLine(string line)
        {
            throw new NotImplementedException();
        }
        private TableSchema GetTableSchema(string line)
        {
            throw new NotImplementedException();
        }

        private Column GetColumn(string line)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
