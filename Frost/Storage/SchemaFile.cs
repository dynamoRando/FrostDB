using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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
        private DbSchema2 _dbSchema;
        private TableSchema2 _tableSchema;
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
            _dbSchema = new DbSchema2();

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
        /// Saves the specified schema to disk
        /// </summary>
        /// <param name="schema">The current database schema</param>
        public void Save(DbSchema2 schema)
        {
            using (var file = new StreamWriter(FileName()))
            {
                file.WriteLine("version " + VersionNumber.ToString());
                foreach (var table in schema.Tables)
                {
                    // table tableId tableName
                    file.WriteLine($"table {table.TableId.ToString()} {table.Name}");
                    foreach (var column in table.Columns)
                    {
                        // column columnName columnDataType
                        file.WriteLine($"column {column.Name} {column.DataType}");
                    }
                }
            }
        }

        /// <summary>
        /// Returns the database schema from the file (for this database)
        /// </summary>
        /// <returns>Database schema for this database.</returns>
        public DbSchema2 GetDbSchema()
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

        /// <summary>
        /// Checks the version number of this file. If it is not set, it will default to Version 1
        /// </summary>
        private void SetVersionNumberIfBlank()
        {
            if (VersionNumber == 0)
            {
                VersionNumber = StorageFileVersions.SCHEMA_FILE_VERSION_1;
            }
        }

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
                    _tableSchema = GetTableSchema(line);
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

        /// <summary>
        /// Creates a new table schema from the text line
        /// </summary>
        /// <param name="line">The line from the file</param>
        /// <returns>A table schema object</returns>
        private TableSchema2 GetTableSchema(string line)
        {
            // table tableId tableName
            TableSchema2 result;
            var items = line.Split(" ");
            {
                result = new TableSchema2(Convert.ToInt32(items[1]), items[2], _dbSchema.DatabaseName, _dbSchema.DatabaseId);
            }

            return result;
        }

        private static ColumnSchema GetColumn(string line)
        {
            // column columnName columnDataType
            ColumnSchema result = null;
            var items = line.Split(" ");
            {
                result = new ColumnSchema(items[1], items[2]);
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
