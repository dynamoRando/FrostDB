using System;

namespace Harness
{
    class Program
    {
        

        static void Main(string[] args)
        {
            var app = new App();

            Console.WriteLine("FrostDB Harness");

            while (app.Running)
            {
                app.PromptForMode();
            }
        }
    }
}
