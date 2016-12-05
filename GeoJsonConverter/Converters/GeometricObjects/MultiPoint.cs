using System;
using System.Collections.Generic;
using System.Linq;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that denotes a multi-point.</summary>
    /// <seealso cref="IGeometricObject" />
    public class MultiPoint : IGeometricObject, IEquatable<MultiPoint>
    {
        /// <summary>An list of <see cref="Point"/> objects that shape this <see cref="MultiPoint"/>.</summary>
        public IEnumerable<Point> Points { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MultiPoint"/> class.</summary>
        public MultiPoint()
        {
            Points = new List<Point>();
        }

        /// <summary>Determines whether the specified <see cref="MultiPoint" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="MultiPoint"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="MultiPoint"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(MultiPoint other)
        {
            if (other == null) return false;

            return Points.SequenceEqual(other.Points);
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as MultiPoint);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Points.GetHashCode() * 397);
            }
        }
    }
}
