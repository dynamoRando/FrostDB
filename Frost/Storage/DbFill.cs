using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DbFill : IDbFill
    {
        public DbSchema Schema { get; set; }
    }
}
