using FrostDB;
using System;
using System.Collections.Generic;

public class DeleteQueryPlanGenerator
{
    #region Private Fields
    private Process _process;
    int _level = 0;
    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public DeleteQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(DeleteStatement statement)
    {
        var result = new QueryPlan();
        result.Steps.AddRange(GetWhereClauseSteps(statement));
        result.Steps.AddRange(GetDeleteSteps(statement));

        return result;
    }
    #endregion

    #region Private Methods
    private List<IPlanStep> GetDeleteSteps(DeleteStatement statement)
    {
        var result = new List<IPlanStep>();

        // when creating delete steps, need to specify database name and table name

        throw new NotImplementedException();

        return result;
    }
    private List<IPlanStep> GetWhereClauseSteps(DeleteStatement statement)
    {
        var result = new List<IPlanStep>();
        if (statement.HasWhereClause)
        {
            var whereGenerator = new WhereClausePlanGenerator(_process, _level);
            result.AddRange(whereGenerator.GetPlanSteps(statement.WhereClause));
        }

        return result;
    }
    #endregion
}