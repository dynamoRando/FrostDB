using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FrostDB
{
    [Serializable]
    public class Location : ILocation, IFrostObject, ISerializable
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        public string Url { get; set; }
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected Location(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Id = (Guid?)serializationInfo.GetValue
               ("LocationId", typeof(Guid?));
            Name = (string)serializationInfo.GetValue
                ("LocationName", typeof(string));
            IpAddress = (string)serializationInfo.GetValue("LocationIpAddress", typeof(string));
            PortNumber = (int)serializationInfo.GetValue("LocationPortNumber", typeof(int));
            Url = (string)serializationInfo.GetValue
                ("LocationUrl", typeof(string));
        }

        public Location(Guid? id, string ipAddress, int portNumber, string name)
        {
            Id = id;
            IpAddress = ipAddress;
            PortNumber = portNumber;
            Name = name;
        }
        #endregion

        #region Public Methods
        public bool IsLocal()
        {
            if (IpAddress.Contains("127.0.0.1") || Url.Contains("localhost") || (IpAddress == Process.GetLocation().IpAddress && PortNumber == Process.GetLocation().PortNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("LocationId", Id.Value, typeof(Guid?));
            info.AddValue("LocationName", Name, typeof(string));
            info.AddValue("LocationIpAddress", IpAddress, typeof(string));
            info.AddValue("LocationPortNumber", PortNumber, typeof(int));
            info.AddValue("LocationUrl", Url,
                typeof(string));
        }

       
        #endregion

        #region Private Methods
        #endregion
    }
}
