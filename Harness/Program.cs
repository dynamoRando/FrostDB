using System;

namespace Harness
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("FrostDB Harness");

            var app = new App();
            app.PromptForMode();

            while (app.Running)
            {
                app.Prompt("noting to do. Exit?");
            }
        }
    }
}
