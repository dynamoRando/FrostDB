﻿using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB
{
    public class ConfigurationManager : IManager, IConfigurationManager<FrostConfiguration>
    {
        public void SaveConfiguration(FrostConfiguration config)
        {
            var seralizer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(config.FileLocation))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                seralizer.Serialize(writer, config);
            }
        }

        public FrostConfiguration LoadConfiguration(string configFileLocation)
        {
            var json = File.ReadAllText(configFileLocation);
            return JsonConvert.DeserializeObject<FrostConfiguration>(json);
        }
    }
}
