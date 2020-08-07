using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to load a database from disk
    /// </summary>
    public class DbFill : IDbFill
    {
        public DbSchema Schema { get; set; }
    }
}
