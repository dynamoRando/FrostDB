using FrostDB;
using System;
using System.Collections.Generic;
using System.Linq;

public class DeleteQueryPlanGenerator
{
    #region Private Fields
    private Process _process;
    private QueryPlan _plan;
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
        _plan = new QueryPlan();
        _plan.Steps.AddRange(GetWhereClauseSteps(statement));
        _plan.Steps.AddRange(GetDeleteSteps(statement));

        return _plan;
    }
    #endregion

    #region Private Methods
    private List<IPlanStep> GetDeleteSteps(DeleteStatement statement)
    {
        var result = new List<IPlanStep>();

        var step = new DeleteStep();
        step.DatabaseName = statement.DatabaseName;
        step.TableName = statement.Tables.First();

        if (statement.HasWhereClause)
        {
            // need to delete only the rows in the where clause
            step.InputStep = QueryPlanGeneratorUtility.GetMaxStep(_plan.Steps);
            step.Level = step.InputStep.Level++;
            var rows = step.InputStep.GetResult(_process, statement.DatabaseName).Rows;
            step.DeletedRows = rows;
        }
        else
        {
            // delete everything
            step.ShouldDeleteAllRows = true;
        }

        result.Add(step);

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