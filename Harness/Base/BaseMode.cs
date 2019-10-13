using FrostDB.Base;
using Harness.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Base
{
    public abstract class BaseMode : IMode
    {
        public App App { get; set; }
        public Process Process => App.Process;

        public BaseMode(App app)
        {
            App = app;
        }

        public abstract void Prompt();
    }
}
