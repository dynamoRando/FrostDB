using FrostDB;
using FrostDB.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class SelectQueryPlanGenerator
{
    #region Private Fields
    SelectStatement _statement;
    Process _process;
    int _level = 0;
    #endregion

    #region Public Properties
    #endregion

    #region Protected Methods
    #endregion

    #region Events
    #endregion

    #region Constructors
    public SelectQueryPlanGenerator()
    { }

    public SelectQueryPlanGenerator(Process process)
    {
        _process = process;
    }
    #endregion

    #region Public Methods
    // TO DO: refactor
    public QueryPlan GeneratePlan(SelectStatement statement)
    {
        _statement = statement;
        var plan = new QueryPlan();

        if (statement.HasWhereClause)
        {
            var whereClauseGenerator = new WhereClausePlanGenerator(_process, _level);
            plan.Steps.AddRange(whereClauseGenerator.GetPlanSteps(statement.WhereClause));
        }
        else
        {
            plan.Steps.Add(GetTableStep(statement));
        }

        // TO DO: We should make the final rows equal the columns in the SELECT statement
        plan.Columns = statement.SelectList;

        int maxStep = 0;
        var columnOutput = new ColumnOutputStep();
        foreach (var step in plan.Steps)
        {
            if (step.Level > maxStep)
            {
                maxStep = step.Level;
            }
        }

        columnOutput.Level = maxStep + 1;

        return plan;
    }
    #endregion

    #region Private Methods
    private TableStep GetTableStep(SelectStatement statement)
    {
        var step = new TableStep(statement);
        step.TableName = statement.Tables.First();
        step.Columns.AddRange(statement.SelectList);

        return step;
    }

    #endregion
}
