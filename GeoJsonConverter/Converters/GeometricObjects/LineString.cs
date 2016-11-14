using System;
using System.Linq;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that denotes a line string.</summary>
    /// <seealso cref="IGeometricObject" />
    public class LineString : IGeometricObject, IEquatable<LineString>
    {
        /// <summary>An ordered list of <see cref="Point"/> that shape this <see cref="LineString"/>.</summary>
        public IOrderedEnumerable<Point> Points { get; set; }
        
        /// <summary>Determines whether the specified <see cref="LineString" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="LineString"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="Point"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(LineString other)
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
            return Equals(obj as LineString);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
