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
        private DbSchema _dbSchema;
        private TableSchema _tableSchema;
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
            _dbSchema = new DbSchema();
            LoadFile();
        }
        #endregion

        #region Public Methods
        public DbSchema GetDbSchema()
        {
            return _dbSchema;
        }
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
            _tableSchema = null;

            var file = Path.Combine(_schemaFileFolder, _databaseName + "." + _schemaFileExtension);
            var lines = File.ReadAllLines(file).ToList();
            lines.ForEach(l => ParseLine(l));
        }

        private void ParseLine(string line)
        {
            if (line.StartsWith("version"))
            {
                ParseVersion(line);
            }
            
            if (line.StartsWith("table"))
            {
                if (_tableSchema is null)
                {
                    _tableSchema = new TableSchema();
                }
                else
                {
                    _dbSchema.Tables.Add(_tableSchema);
                    _tableSchema = GetTableSchema(line);
                }    
            }
            
            if (line.StartsWith("column"))
            {
                var column = GetColumn(line);
                _tableSchema.Columns.Add(column);
            }
        }
        private TableSchema GetTableSchema(string line)
        {
            // table tableId tableName
            var result = new TableSchema();

            var items = line.Split(" ");
            {
                Guid item;
                if (Guid.TryParse(items[1], out item))
                {
                    result.TableId = item;
                }
                result.TableName = items[2];
            }

            return result;
        }

        private Column GetColumn(string line)
        {
            // column columnid columnName columnDataType
            Column result = null;
            var items = line.Split(" ");
            {
                Guid item;
                if (Guid.TryParse(items[1], out item))
                {
                    result = new Column(items[2], Type.GetType(items[3]), item);
                }
            }
            return result;
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
