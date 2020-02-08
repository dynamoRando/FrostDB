using System;
using FrostCommon;
using Newtonsoft.Json;

namespace FrostDB.Extensions
{
    public static class Extensions
    {
        public static Row GetData(this RowReference reference)
        {
            //Guid? id = reference.Id;
            /*
             * write whatever logic needed here to connect to the remote location
             * for this row
             * and get the row based on the reference.Id
             * will likely need a reference to the comm service to get data in
             * an async manner
             */

            throw new NotImplementedException();
        }

        public static void SendResponse(this Message message)
        {
            // once a message has been processed, generate the appropriate response message and send it
            Message m = MessageResponse.Create(message);
            NetworkReference.SendMessage(m);
        }

        public static bool IsLocal(this Location location)
        {
            if (location.IpAddress.Contains("127.0.0.1") || location.Url.Contains("localhost") || (location.IpAddress == Process.GetLocation().IpAddress && location.PortNumber == Process.GetLocation().PortNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
