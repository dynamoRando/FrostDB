using FrostCommon;
using FrostCommon.ConsoleMessages;
using System;
using FrostLocation = FrostCommon.ConsoleMessages.LocationInfo;

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

        public static void SendResponse(this Message message, MessageResponse responder, Process process)
        {
            // once a message has been processed, generate the appropriate response message and send it
            Message m = responder.Create(message);
            process.Network.SendMessage(m);
        }

        public static bool IsLocal(this Location location, Process process)
        {
            if (location.IpAddress == process.GetLocation().IpAddress && location.PortNumber == process.GetLocation().PortNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static FrostLocation Convert(this Location location)
        {
            var info = new FrostLocation();
            info.IpAddress = location.IpAddress;
            info.PortNumber = location.PortNumber;

            return info;
        }

        public static Location Convert(this FrostLocation location)
        {
            return new Location(Guid.NewGuid(), location.IpAddress, location.PortNumber, string.Empty);
        }

        public static ContractInfo Convert(this Contract contract)
        {
            var info = new ContractInfo();

            info.ContractDescription = contract.ContractDescription;
            info.DatabaseName = contract.DatabaseName;
            info.ContractVersion = contract.ContractVersion;
            info.ContractId = contract.ContractId;
            info.Location = contract.DatabaseLocation.Convert();
            info.DatabaseId = contract.DatabaseId;
            info.Schema = DbSchemaMapper.Map(contract.DatabaseSchema);

            return info;
        }

        public static Contract Convert(this ContractInfo info, Process process)
        {
            var contract = new Contract(process);
            throw new NotImplementedException();
            return contract;
        }

    }
}
