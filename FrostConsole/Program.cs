using System;

namespace FrostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = App.Prompt("Would you like to (s) - start FrostDb or (c) - configure your instance? (e) to exit.");

            do
            {
                switch (result)
                {
                    case "s":
                        App.Run();
                        break;
                    case "c":
                        App.Configure();
                        break;
                    case "e":
                        App.Shutdown();
                        break;
                }
            } while (result != "e");
        }
    }
}
