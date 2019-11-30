using System;

namespace FrostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string result;

            do
            {
                result = App.Prompt("Would you like to (s) - start FrostDb or (c) - configure your instance? (e) to exit.");
                switch (result)
                {
                    case "s":
                        App.Startup();

                        while (App.IsRunning)
                        {
                            result = App.Prompt("FrostDb is running, press (e) key to exit");

                            if (result == "e")
                            {
                                App.Shutdown();
                                break;
                            }
                        }

                        break;
                    case "c":
                        App.Configure();
                        break;
                    case "e":
                        break;
                }
            } while (result != "e");

            Console.WriteLine("Console finished running.");
        }
    }
}
