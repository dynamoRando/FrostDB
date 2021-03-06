﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrostDB
{
    public class QueryPlanGenerator
    {
        #region Private Fields
        Process _process;
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        public QueryPlanGenerator(Process process)
        {
            _process = process;
        }
        #endregion

        #region Public Methods
        public QueryPlan GeneratePlan(FrostIDDLStatement statement, string databaseName)
        {
            QueryPlan plan = GeneratePlan(statement);

            if (statement is CreateDatabaseStatement)
            {
                plan.DatabaseName = (statement as CreateDatabaseStatement).DatabaseName;
            }
            else
            {
                plan.DatabaseName = databaseName;
            }

            return plan;
        }

        public QueryPlan GeneratePlan(FrostIDMLStatement statement, string databaseName)
        {
            QueryPlan plan = GeneratePlan(statement);
            plan.DatabaseName = databaseName;
            return plan;
        }

        public QueryPlan GeneratePlan(FrostIDDLStatement statement)
        {
            var plan = new QueryPlan();

            if (statement is CreateTableStatement)
            {
                plan = new CreateTableQueryPlanGenerator(_process).GeneratePlan((statement as CreateTableStatement));
            }

            if (statement is CreateDatabaseStatement)
            {
                plan = new CreateDatabaseQueryPlanGenerator(_process).GeneratePlan((statement as CreateDatabaseStatement));
            }

            return plan;
        }

        public QueryPlan GeneratePlan(FrostIDMLStatement statement)
        {
            var plan = new QueryPlan();
            if (statement is SelectStatement)
            {
                plan = new SelectQueryPlanGenerator(_process).GeneratePlan((statement as SelectStatement));
            }

            if (statement is InsertStatement)
            {
                plan = new InsertQueryPlanGenerator(_process).GeneratePlan((statement as InsertStatement));
            }

            if (statement is UpdateStatement)
            {
                plan = new UpdateQueryPlanGenerator(_process).GeneratePlan((statement as UpdateStatement));
            }

            if (statement is DeleteStatement)
            {
                plan = new DeleteQueryPlanGenerator(_process).GeneratePlan((statement as DeleteStatement));
            }

            return plan;
        }
        #endregion

        #region Private Methods
        #endregion
    }
}