using System;
using System.Collections.Generic;
using System.Linq;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that is a figure bounded by a finite chain of straight line segments closing in a loop to form a closed chain.</summary>
    /// <seealso cref="IGeometricObject" />
    public class Polygon : IGeometricObject, IEquatable<Polygon>
    {
        /// <summary>An ordered list of line segments (<see cref="LineString"/>) that form this <see cref="Polygon"/>.</summary>
        public IEnumerable<LineString> LineSegments { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Polygon"/> class.</summary>
        public Polygon()
        {
            LineSegments = new List<LineString>();
        }

        /// <summary>Determines whether the specified <see cref="Polygon" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="Polygon"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="Polygon"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(Polygon other)
        {
            if (other == null) return false;

            return LineSegments.SequenceEqual(other.LineSegments);
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Polygon);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (LineSegments.GetHashCode() * 397);
            }
        }
    }
}
