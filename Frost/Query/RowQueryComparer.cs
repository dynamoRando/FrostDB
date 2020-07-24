using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FrostDB
{
    public class RowQueryComparer : IEqualityComparer<Row>
    {
        public bool Equals(Row x, Row y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            bool sizesEqual = false;
            bool valuesEqual = true;

            if (x.Values.Count == y.Values.Count)
            {
                sizesEqual = true;
                foreach (var xvalue in x.Values)
                { 
                    foreach(var yvalue in y.Values)
                    {
                        if (xvalue.ColumnName == yvalue.ColumnName && 
                            xvalue.ColumnType == yvalue.ColumnType)
                        {
                            Debug.WriteLine(xvalue.Value.ToString());
                            Debug.WriteLine(yvalue.Value.ToString());
                            if (xvalue.Value.ToString() != yvalue.Value.ToString())
                            {
                                valuesEqual = false;
                                break;
                            }
                        }
                    }
                    if (!valuesEqual)
                    {
                        break;
                    }
                }
            }

            return sizesEqual && valuesEqual;
        }

        public int GetHashCode(Row obj)
        {
            if (obj is null)
            {
                return 0;
            }

            int id = obj.Id.GetHashCode();
            int valueHash = 0;
            int resultHashCode = 0;

            foreach (var value in obj.Values)
            {
                if (value != null)
                {
                    valueHash = value.Value.GetHashCode();
                }
                else
                {
                    valueHash = 0;
                }
                resultHashCode += valueHash;
            }

            return id ^ resultHashCode;
        }
    }
}
