using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrostDB
{
    public static class Json
    {
        public static bool TryParse(string json, out Message message)
        {
            try
            {
                message = JsonConvert.DeserializeObject<Message>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                });

                return true;
            }
            catch (Exception e)
            {
                message = null;
                return false;
            }
        }

        public static string SeralizeMessage(Message message)
        {
            var data = string.Empty;
            data = JsonConvert.SerializeObject(message);
            return data;
        }
    }
}
