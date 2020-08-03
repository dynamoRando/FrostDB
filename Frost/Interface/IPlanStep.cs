using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

public interface IPlanStep
{
    Guid Id { get; set; }
    int Level { get; set; }
    StepResult GetResult(Process process, string databaseName);
    string GetResultText();
}
