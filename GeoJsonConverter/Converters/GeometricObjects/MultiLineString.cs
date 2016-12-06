using System;
using System.Collections.Generic;
using System.Linq;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that contains a collection of zero or more <see cref="LineString"/> objects.</summary>
    /// <seealso cref="IGeometricObject" />
    public class MultiLineString : IGeometricObject, IEquatable<MultiLineString>
    {
        /// <summary>A list of <see cref="LineString"/> objects that shape this <see cref="MultiLineString"/>.</summary>
        public IEnumerable<LineString> LineStrings { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MultiLineString"/> class.</summary>
        public MultiLineString()
        {
            LineStrings = new List<LineString>();
        }

        /// <summary>Determines whether the specified <see cref="MultiLineString" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="MultiLineString"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="MultiLineString"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(MultiLineString other)
        {
            if (other == null) return false;

            return LineStrings.SequenceEqual(other.LineStrings);
        }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as MultiLineString);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (LineStrings.GetHashCode() * 397);
            }
        }
    }
}
