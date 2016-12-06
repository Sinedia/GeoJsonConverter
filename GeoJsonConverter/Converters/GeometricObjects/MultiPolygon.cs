using System;
using System.Collections.Generic;
using System.Linq;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that contains a collection of zero or more <see cref="Polygon"/> objects.</summary>
    /// <seealso cref="IGeometricObject" />
    public class MultiPolygon : IGeometricObject, IEquatable<MultiPolygon>
    {
        /// <summary>A list of <see cref="Polygon"/> objects that shape this <see cref="MultiPolygon"/>.</summary>
        public IEnumerable<Polygon> Polygons { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MultiPolygon"/> class.</summary>
        public MultiPolygon()
        {
            Polygons = new List<Polygon>();
        }

        /// <summary>Determines whether the specified <see cref="MultiPolygon" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="MultiPolygon"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="MultiPolygon"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(MultiPolygon other)
        {
            if (other == null) return false;

            return Polygons.SequenceEqual(other.Polygons);
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as MultiPolygon);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Polygons.GetHashCode() * 397);
            }
        }
    }
}
