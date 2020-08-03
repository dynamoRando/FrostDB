using System;
using System.Collections.Generic;
using System.Text;


public interface IPlanResult
{
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }
}
