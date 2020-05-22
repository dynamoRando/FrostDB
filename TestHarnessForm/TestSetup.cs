using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace TestHarnessForm
{
    [Serializable]
    public class TestProcess
    {
        public string IPAddress { get; set; }
        public int DataPort { get; set; }
        public int ConsolePort { get; set; }
        public int ConsoleResponsePort { get; set; }
        public string RootDirectory { get; set; }
    }

    [Serializable]
    public class TestSetup
    {
        public List<TestProcess> Items { get; set; }
        public TestSetup()
        {
            Items = new List<TestProcess>();
        }
    }

    public class TestSetupHarness
    {
        public void SaveSetup(string fileLocation, TestSetup setup)
        {
            var text = JsonConvert.SerializeObject(setup);
            File.WriteAllText(fileLocation, text);
        }

        public TestSetup LoadSetup(string fileLocation)
        {
            var text = File.ReadAllText(fileLocation);
            return JsonConvert.DeserializeObject<TestSetup>(text);
        }
    }
}
