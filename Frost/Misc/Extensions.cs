using FrostCommon;
using FrostCommon.ConsoleMessages;
using FrostCommon.Net;
using FrostDB;
using System;
using System.Collections.Generic;
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

        public static RowValue Convert(this UpdateQueryColumnParameters parameters)
        {
            return new RowValue
            {
                ColumnName = parameters.ColumnName,
                ColumnType = parameters.ColumnType,
                Value = parameters.Value
            };
        }

        public static bool IsLocal(this RowReference reference, Process process)
        {
            if (reference.Participant.Location.IpAddress == process.GetLocation().IpAddress && reference.Participant.Location.PortNumber == process.GetLocation().PortNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static FrostPromptPlan Convert(this QueryPlan plan)
        {
            var result = new FrostPromptPlan();

            foreach(var step in plan.Steps)
            {
                result.PlanText += step.GetResultText() + Environment.NewLine;
            }

            return result;
        }

    }
}
