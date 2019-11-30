using Newtonsoft.Json;
using System;
using FrostCommon;

namespace FrostDB
{
    public static class Json
    {
        public static bool TryParse(string json, out Contract contract)
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

                contract = JsonConvert.DeserializeObject<Contract>(json, set);

                return true;
            }
            catch (Exception e)
            {
                contract = null;
                return false;
            }
        }

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

        public static string SeralizeContract(Contract contract)
        {
            var data = string.Empty;
            data = JsonConvert.SerializeObject(contract);
            return data;
        }
    }
}
