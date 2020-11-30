using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public struct RowStruct
    {
        public int RowId { get; set; }
        public bool IsLocal { get; set; }
        public int RowSize { get; set; }
        public Guid ParticipantId { get; set; }
        public RowValue2[] Values { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("**** RowStruct Debug ****" + Environment.NewLine);
            builder.Append($"RowId: {RowId.ToString()} {Environment.NewLine}");
            builder.Append($"IsLocal: {IsLocal.ToString()} {Environment.NewLine}");
            builder.Append($"RowSize: {RowSize.ToString()} {Environment.NewLine}");

            if (ParticipantId != null)
            {
                if (ParticipantId != Guid.Empty)
                {
                    builder.Append($"ParticipantId: {ParticipantId.ToString()} {Environment.NewLine}");
                }
            }

            if (Values != null)
            {
                foreach (var value in Values)
                {
                    builder.Append($"{value.Column.Name} : {value.Value} {Environment.NewLine}");
                }
            }

            builder.Append(Environment.NewLine);
            builder.Append("**** End RowStruct Debug ****" + Environment.NewLine);
            return builder.ToString();
        }
    }
}
