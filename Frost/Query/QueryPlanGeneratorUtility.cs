using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FrostDB
{
    public static class QueryPlanGeneratorUtility
    {
        public static IPlanStep GetMaxStep(List<IPlanStep> steps)
        {
            int level = 0;
            level = GetMaxLevel(steps);

            return steps.Where(s => s.Level == level).FirstOrDefault();
        }
        public static int GetMaxLevel(List<IPlanStep> steps)
        {
            int maxLevel = 0;
            foreach (var step in steps)
            {
                if (step.Level > maxLevel)
                {
                    maxLevel = step.Level;
                }
            }

            return maxLevel;
        }
    }  
}
