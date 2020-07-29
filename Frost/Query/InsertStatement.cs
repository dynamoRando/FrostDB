﻿using System;
using System.Collections.Generic;
using System.Text;
using FrostCommon;

namespace FrostDB
{
    public class InsertStatement : IStatement
    {
        public List<string> Tables { get; set; }
        public WhereClause WhereClause { get; set; }
        public string RawStatement { get; set; }
        public bool HasWhereClause { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<InsertStatementGroup> InsertValues { get; set; }

        public string ParticipantString { get; set; }

        public Location Location { get; set; }

        public InsertStatement()
        {
            Tables = new List<string>();
            WhereClause = new WhereClause();
            ColumnNames = new List<string>();
            InsertValues = new List<InsertStatementGroup>();
        }
    }

    public class InsertStatementGroup
    {
        public List<string> Values { get; set; }

        public InsertStatementGroup()
        {
            Values = new List<string>();
        }
    }
}
