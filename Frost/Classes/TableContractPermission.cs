using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using FrostDB.Enum;
using System.Runtime.Serialization;

namespace FrostDB
{
    [Serializable]
    public class TableContractPermission : IContractPermission, ISerializable
    {
        #region Private Fields
        private Guid? _tableId;
        private Cooperator _cooperator;
        private List<TablePermission> _permissions;
        #endregion

        #region Public Properties
        public Guid? TableId => _tableId;
        public Cooperator Cooperator => _cooperator;
        public List<TablePermission> Permissions => _permissions;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        protected TableContractPermission(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _cooperator = (Cooperator)serializationInfo.GetValue("TableContractPermissionCooperator", typeof(Cooperator));
            _tableId = (Guid?)serializationInfo.GetValue("TableContractPermissionTableId", typeof(Guid?));
            _permissions = (List<TablePermission>)serializationInfo.GetValue("TableContractPermissions", typeof(List<TablePermission>));
        }
        public TableContractPermission(Guid? tableId, Cooperator cooperator, List<TablePermission> permissions)
        {
            _tableId = tableId;
            _cooperator = cooperator;
            _permissions = permissions;
        }
        #endregion

        #region Public Methods
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TableContractPermissionCooperator", _cooperator, typeof(Cooperator));
            info.AddValue("TableContractPermissionTableId", _tableId, typeof(Guid?));
            info.AddValue("TableContractPermissions", _permissions, typeof(List<TablePermission>));
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
