using Harness.Interface;
using System;

namespace Harness
{
    class Program
    {

        static void Main(string[] args)
        {
            IMode mode;
            Console.WriteLine("FrostDB Harness");

            var app = new App();
            app.PromptForStartup();

            while (app.Running)
            {
                mode = app.PromptForMode();

                if (app.Running && !(mode is null))
                {
                    mode.Prompt();
                }
            }

            app.Shutdown();
        }
    }
}
