using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    class CreateDatabaseQueryPlanGenerator
    {
        private Process _process;
        public CreateDatabaseQueryPlanGenerator(Process process)
        {
            _process = process;
        }

        public QueryPlan GeneratePlan(CreateDatabaseStatement statement)
        {
            var step = new CreateDatabaseStep();
            step.DatabaseName = statement.DatabaseName;
            step.IsValid = true;
            var plan = new QueryPlan();
            plan.Steps.Add(step);

            return plan;
        }
    }
}
