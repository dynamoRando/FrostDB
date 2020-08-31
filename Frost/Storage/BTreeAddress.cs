using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Used to identify which table a B-Tree belongs to
    /// </summary>
    class BTreeAddress : IEquatable<BTreeAddress>
    {
        public int DatabaseId { get; set;}
        public int TableId { get; set; }

        public bool Equals(BTreeAddress other)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return (DatabaseId == other.DatabaseId) && (TableId == other.TableId);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BTreeAddress);
        }

        public static bool operator ==(BTreeAddress lhs, BTreeAddress rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(BTreeAddress lhs, BTreeAddress rhs)
        {
            return !(lhs == rhs);
        }
    }
}
