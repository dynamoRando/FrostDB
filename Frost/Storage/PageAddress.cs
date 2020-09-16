using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    /// <summary>
    /// Represents a specific Page Address: DatabaseId, TableId, PageId
    /// </summary>
    public struct PageAddress : IEquatable<PageAddress>
    {
        public int DatabaseId { get; set; }
        public int TableId { get; set; }
        public int PageId { get; set; }
        public BTreeAddress TreeAddress => new BTreeAddress { DatabaseId = this.DatabaseId, TableId = this.TableId };

        public bool Equals(PageAddress other)
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

            return (this.DatabaseId == other.DatabaseId) && (this.TableId == other.TableId) && (this.PageId == other.PageId);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj, this);
        }

        public static bool operator ==(PageAddress lhs, PageAddress rhs)
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

        public static bool operator !=(PageAddress lhs, PageAddress rhs)
        {
            return !(lhs == rhs);
        }
    }
}
