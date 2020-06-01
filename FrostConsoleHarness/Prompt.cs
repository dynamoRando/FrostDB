using System;

namespace FrostConsoleHarness
{
    static class Prompt
    {
        public static string For(string outputMessage)
        {
            Console.WriteLine(outputMessage);
            return Console.ReadLine();
        }
    }
}