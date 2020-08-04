using System;
using FrostDB;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrostDB.Query;

public class UpdateQueryPlanGenerator
{
    #region Private Fields
    private Process _process;
    private int _level;
    #endregion

    #region Public Properties
    #endregion

    #region Constructors
    public UpdateQueryPlanGenerator(Process process)
    {
        _process = process;
        _level = 0;
    }
    #endregion

    #region Public Methods
    public QueryPlan GeneratePlan(UpdateStatement statement)
    {
        var result = new QueryPlan();
        statement.ParseElements();
        // TO DO: We need to extract out from the Select Plan Generator 
        // a generic WhereClausePlanGenerator because the behavior should be the same, i.e.
        // generate the plan steps to find the rows that apply to a WHERE clause
        // and then either return those rows or take an action on them (DELETE, or UPDATE)
        result.Steps.AddRange(GetWhereClauseSteps(statement));
        _level = QueryPlanGeneratorUtility.GetMaxLevel(result.Steps);
        result.Steps.AddRange(GetUpdateSteps(statement, result.Steps));
        result.OriginalStatement = statement;
        return result;
    }
    #endregion

    #region Private Methods
   
    // TO DO: We need to figure out how to input the rows we wish to affect
    private List<IPlanStep> GetUpdateSteps(UpdateStatement statement, List<IPlanStep> existingSteps)
    {
        var result = new List<IPlanStep>();

        foreach(var element in statement.Elements)
        {
            var step = new UpdateStep();

            if (statement.HasWhereClause)
            {
                step.InputStep = QueryPlanGeneratorUtility.GetMaxStep(existingSteps);
            }

            step.TableName = element.TableName;
            step.DatabaseName = element.DatabaseName;
            step.ColumnName = element.ColumnName;
            step.Value = element.Value;
            _level++;
            step.Level = _level;
            result.Add(step);
        }

        return result;
    }
    private List<IPlanStep> GetWhereClauseSteps(UpdateStatement statement)
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