using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sinedia.Json.Converters.GeometricObjects;

namespace Sinedia.Json.Converters.TokenConverters
{
    internal class TokenToGeometricObjectConverter : IGeometricObject
    {
        public object Convert(FeatureType featureType, JToken geoJsonCoordinates)
        {
            // Create the result value
            IGeometricObject geometricObject = null;

            // Process (convert) the coordinates based on the found feature type
            switch (featureType)
            {
                case FeatureType.Point:
                    geometricObject = ConvertTokenToPoint(geoJsonCoordinates);

                    break;

                case FeatureType.LineString:
                    // TODO
                    break;

                case FeatureType.Polygon:
                    // TODO
                    break;

                case FeatureType.MultiPoint:
                    // TODO
                    break;

                case FeatureType.MultiLineString:
                    // TODO
                    break;

                case FeatureType.MultiPolygon:
                    // TODO
                    break;
            }

            // Return the geometric object
            return geometricObject;
        }

        /// <summary>Converts a point.</summary>
        /// <param name="point">The representation of a point.</param>
        /// <returns>A WKT representation of the same point.</returns>
        /// <exception cref="ArgumentNullException">point</exception>
        private static Point ConvertTokenToPoint(JToken point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (point.Count() != 2) throw new ArgumentException(@"There always need to be an x and y coordinate.", nameof(point));

            return new Point() {X = point.Values<double>().First(), Y = point.Values<double>().Last()};
        }
    }
}
