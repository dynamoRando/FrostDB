using System;
using FrostDB;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UpdateQueryPlanGenerator
{
    #region Private Fields
    private Process _process;

    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public UpdateQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(UpdateStatement statement)
    {
        var result = new QueryPlan();
        statement.ParseElements();
        result.Steps.AddRange(GetWhereClauseSteps(statement));
        result.Steps.AddRange(GetUpdateSteps(statement));
        return result;
    }
    #endregion

    #region Private Methods
    private List<IPlanStep> GetUpdateSteps(UpdateStatement statement)
    {
        var result = new List<IPlanStep>();

        foreach(var element in statement.Elements)
        {
            var step = new UpdateStep();
            step.TableName = element.TableName;
            step.DatabaseName = element.DatabaseName;
            step.ColumnName = element.ColumnName;
            step.Value = element.Value;
            result.Add(step);
        }

        return result;
    }
    private List<IPlanStep> GetWhereClauseSteps(UpdateStatement statement)
    {
        var result = new List<IPlanStep>();

        if (statement.HasWhereClause)
        {
            foreach (var condition in statement.WhereClause.Conditions)
            {
               var step = new SearchStep(condition);
               step.DatabaseName = statement.DatabaseName;
               statement.MaxStepLevel++;
               step.Level = statement.MaxStepLevel;
               result.Add(step);
            }
        }
        return result;
    }
    #endregion
}