using FrostDB.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public static class Json
    {
        public static bool TryParse(string json, out IMessage message)
        {
            try
            {
                message = JsonConvert.DeserializeObject<IMessage>(json);
                return true;
            }
            catch (Exception e)
            {
                message = null;
                return false;
            }
        }
    }
}
