using System;
using System.Collections.Generic;

namespace Sinedia.Json.Converters.GeometricObjects
{
    /// <summary>A geometric object that denotes a point.</summary>
    /// <seealso cref="IGeometricObject" />
    public class Point : IGeometricObject, IEquatable<Point>, IEqualityComparer<Point>
    {
        /// <summary>The x coordinate of this point.</summary>
        public double X { get; set; }

        /// <summary>The y coordinate of this point.</summary>
        public double Y { get; set; }

        //public double? Z { get; set; }

        /// <summary>Determines whether the specified <see cref="object" />, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="object" /> is equal to the <paramref name="obj" /> parameter; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Point);
        }

        /// <summary>Determines whether the specified <see cref="Point" />, is equal to the other specified <see cref="Point" />.</summary>
        /// <param name="x">The first <see cref="Point"/> to compare.</param>
        /// <param name="y">The second <see cref="Point"/> to compare.</param>
        /// <returns><c>true</c> if the specified <see cref="Point" /> is equal to the other specified <see cref="Point"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Point x, Point y)
        {
            return x.Equals(y);
        }

        /// <summary>Determines whether the specified <see cref="Point" />, is equal to this instance.</summary>
        /// <param name="other">The <see cref="Point"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the current <see cref="Point"/> is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(Point other)
        {
            if (other == null) return false;

            return (Math.Abs(X - other.X) < double.Epsilon) && (Math.Abs(Y - other.Y) < double.Epsilon);
        }
        
        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <param name="obj">The <see cref="Point"/> to get the hash code for.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(Point obj)
        {
            return obj.GetHashCode();
        }
    }
}
