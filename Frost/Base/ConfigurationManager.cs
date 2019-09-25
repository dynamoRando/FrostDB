using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB.Base
{
    public class ConfigurationManager : IManager
    {
        public static void SaveConfiguration(IConfiguration config)
        {
            var seralizer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(config.FileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                seralizer.Serialize(writer, config);
            }
        }

        public static void LoadConfiguration(IConfiguration config, string configFileLocation)
        {
            var json = System.IO.File.ReadAllText(configFileLocation);
            config = JsonConvert.DeserializeObject<IConfiguration>(json);
        }
    }
}
