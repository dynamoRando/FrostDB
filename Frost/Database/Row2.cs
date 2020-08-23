using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class Row2
    {
        /*
         * Row Byte Array Layout:
         * IsLocal ParticipantId
         * [data_col1] [data_col2] [data_colX]
         */
        #region Private Fields
        private byte[] _data;
        private bool _isLocal;
        private int _sizeOfIsLocal;
        private Guid _participantId;
        private List<ColumnSchema> _columns;
        #endregion

        #region Public Properties
        public int SizeOfParticipantId => DatabaseConstants.PARTICIPANT_ID_SIZE;
        public int SizeOfIsLocal => _sizeOfIsLocal;
        public byte[] Data => _data;
        public bool IsLocal => _isLocal;
        public Guid ParticipantId => _participantId;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public Row2() { }
        public Row2(byte[] data, List<ColumnSchema> columns)
        {
            _data = data;
            _columns = columns;
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
