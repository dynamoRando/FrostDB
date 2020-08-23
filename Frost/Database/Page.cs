using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Page
    {
        #region Private Fields
        private int _sizeOfId;
        private byte[] _data;
        #endregion

        #region Public Properties
        public int Id { get; set; }
        public byte[] Data => _data;
        public int SizeOfId => _sizeOfId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Page() 
        {
            _data = new byte[DatabaseConstants.PAGE_SIZE];
        }
        public Page(byte[] data)
        {
            _data = data;
            Id = GetId();
        }

        public Page(byte[] data, int id) : this(data)
        {
            Id = id;
            _data = data;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void SaveId()
        {
            var idData = BitConverter.GetBytes(Id);
            _sizeOfId = idData.Length;
            idData.CopyTo(_data, 0);
        }

        private int GetId()
        {
            var idSpan = new Span<byte>(Data);
            var idBytes = idSpan.Slice(0, _sizeOfId);
            return BitConverter.ToInt32(idBytes);
        }
        #endregion

    }
}
