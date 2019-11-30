using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FrostCommon;

namespace FrostDbClient
{
    internal static class Json
    {
        public static bool TryParse(string json, out Message message)
        {
            try
            {
                var conv = new Newtonsoft.Json.Converters.IsoDateTimeConverter();

                var set = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    Formatting = Formatting.Indented
                };

                set.Converters.Add(conv);

                message = JsonConvert.DeserializeObject<Message>(json, set);

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
