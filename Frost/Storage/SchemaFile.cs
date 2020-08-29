using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace FrostDB
{
    /// <summary>
    /// Contains the schema for a specified database
    /// </summary>
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
        /// <summary>
        /// Creates a new schema file for the specified database 
        /// </summary>
        /// <param name="schemaFileFolder">The location of the schema file folder for the Frost process</param>
        /// <param name="fileExtension">The filename extension for schema files</param>
        /// <param name="databaseName">The name of the database</param>
        public SchemaFile(string schemaFileFolder, string fileExtension, string databaseName)
        {
            _schemaFileExtension = schemaFileFolder;
            _schemaFileExtension = fileExtension;
            _databaseName = databaseName;
            _dbSchema = new DbSchema();

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
        /// Returns the database schema from the file (for this database)
        /// </summary>
        /// <returns>Database schema for this database.</returns>
        public DbSchema GetDbSchema()
        {
            return _dbSchema;
        }

        /// <summary>
        /// Validates the file format of the schema file.
        /// </summary>
        /// <returns>True if the file format is correct, otherwise false.</returns>
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the schema file for this database from disk.
        /// </summary>
        public void Load()
        {
            LoadFile();
        }
        #endregion

        #region Private Methods
        private void CreateFile()
        {
            using (var file = File.Create(FileName()))
            {
                
            }
        }

        /// <summary>
        /// Checks to see if the Schema file exists for this database.
        /// </summary>
        /// <returns>True if the schema file exists, otherwise false.</returns>
        private bool DoesFileExist()
        {
            return File.Exists(FileName());
        }

        /// <summary>
        /// Returns the schema filename for this database.
        /// </summary>
        /// <returns>Returns the schema filename for this database.</returns>
        private string FileName()
        {
            return Path.Combine(_schemaFileFolder, _databaseName + "." + _schemaFileExtension);
        }

        /// <summary>
        /// Loads the schema file from disk into memory for this database
        /// </summary>
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
        private static TableSchema GetTableSchema(string line)
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

        private static Column GetColumn(string line)
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
