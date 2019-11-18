using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB.Interface
{
    public interface ITableSchema
    {
        string TableName { get; set; }
        Guid? TableId { get; set; }
        List<Column> Columns { get; set; }
        bool IsCooperative { get; set; }
        public Guid? Version { get; set; }
    }
}
