﻿using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB.Base
{
    public class ConfigurationManager : IManager, IConfigurationManager<Configuration>
    {
        public void SaveConfiguration(Configuration config)
        {
            var seralizer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(config.FileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                seralizer.Serialize(writer, config);
            }
        }

        public Configuration LoadConfiguration(string configFileLocation)
        {
            var json = File.ReadAllText(configFileLocation);
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }
}
