using FrostDB.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FrostDB
{
    /// <summary>
    /// Represents a "form" to be filled out for a row - the row itself and the participant that row is attached to
    /// </summary>
    /// <seealso cref="FrostDB.Interface.IRowForm" />
     [Serializable]
    public class RowForm : IRowForm, ISerializable
    {
        #region Private Fields
        private Row _row;
        private Participant _participant;
        #endregion

        #region Public Properties
        public Row Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        public Participant Participant => _participant;
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public Guid? DatabaseId { get; set; }
        public bool IsRemoteInsert { get; set; }
        public RowReference Reference { get; set; }
        public List<RowValue> RowValues { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
         protected RowForm(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            _participant = (Participant)serializationInfo.GetValue("Participant", typeof(Participant));
            _row = (Row)serializationInfo.GetValue("Row", typeof(Row));
        }
        public RowForm()
        {
            // default constructor
        }
        public RowForm(Row row, Participant participant)
        {
            _row = row;
            _participant = participant;
            RowValues = new List<RowValue>();
        }

        public RowForm(Row row, Participant participant, RowReference reference, List<RowValue> rowValues)
        {
            _row = row;
            _participant = participant;
            Reference = reference;
            RowValues = rowValues;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Participant", _participant, typeof(Participant));
            info.AddValue("Row", _row, typeof(Row));
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion

    }
}
