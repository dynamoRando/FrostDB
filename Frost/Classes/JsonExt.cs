using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public static class JsonExt
    {
        public static string SeralizeContract(Contract contract)
        {
            var data = string.Empty;
            data = JsonConvert.SerializeObject(contract);
            return data;
        }

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
    }
}
