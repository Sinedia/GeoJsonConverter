using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Sinedia.Json.Converters.GeometricObjects;

namespace Sinedia.Json.Converters.TokenConverters
{
    /// <summary>Responsible for token to geometric object conversions.</summary>
    /// <seealso cref="ITokenConverter" />
    internal class TokenToGeometricObjectConverter : ITokenConverter
    {
        /// <summary>Converts the token accordingly the specified the feature type.</summary>
        /// <param name="featureType">The feature type.</param>
        /// <param name="geoJsonCoordinates">The Geo JSON coordinates.</param>
        /// <returns>An <see cref="IGeometricObject"/> containing the converted values.</returns>
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
                    geometricObject = ConvertTokenToLineString(geoJsonCoordinates);
                    break;

                case FeatureType.Polygon:
                    geometricObject = ConvertTokenToPolygon(geoJsonCoordinates);
                    break;

                case FeatureType.MultiPoint:
                    geometricObject = ConvertTokenToMultiPoint(geoJsonCoordinates);
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

        /// <summary>Converts a token to a multi-point.</summary>
        /// <param name="multiPoint">The representation of a multi-point.</param>
        /// <returns>A <see cref="MultiPoint"/> object.</returns>
        /// <exception cref="ArgumentNullException">lineString</exception>
        private static MultiPoint ConvertTokenToMultiPoint(JToken multiPoint)
        {
            if (multiPoint == null) throw new ArgumentNullException(nameof(multiPoint));

            var points = new List<Point>();

            foreach (var point in multiPoint)
            {
                points.Add(ConvertTokenToPoint(point));
            }

            return new MultiPoint() { Points = points };
        }

        /// <summary>Converts a token to a polygon.</summary>
        /// <param name="polygon">The representation of a polygon.</param>
        /// <returns>A <see cref="Polygon"/> object.</returns>
        /// <exception cref="ArgumentNullException">polygon</exception>
        private static Polygon ConvertTokenToPolygon(JToken polygon)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));

            var lineSegments = new List<LineString>();

            foreach (var shape in polygon)
            {
                lineSegments.Add(ConvertTokenToLineString(shape));
            }

            return new Polygon() { LineSegments = lineSegments };
        }

        /// <summary>Converts a token to a line-string (or a shape inside a polygon) with a list of coordinates.</summary>
        /// <param name="lineString">The representation of a line-string (with a list of coordinates).</param>
        /// <returns>A <see cref="LineString"/> object.</returns>
        /// <exception cref="ArgumentNullException">lineString</exception>
        private static LineString ConvertTokenToLineString(JToken lineString)
        {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));

            var points = new List<Point>();

            foreach (var coordinateList in lineString)
            {
                points.Add(ConvertTokenToPoint(coordinateList));
            }

            return new LineString() { Points = points };
        }

        /// <summary>Converts a token to a point.</summary>
        /// <param name="point">The representation of a point.</param>
        /// <returns>A <see cref="Point"/> object.</returns>
        /// <exception cref="ArgumentNullException">point</exception>
        private static Point ConvertTokenToPoint(JToken point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (point.Count() != 2) throw new ArgumentException(@"There always need to be an x and y coordinate.", nameof(point));

            return new Point() { X = point.Values<double>().First(), Y = point.Values<double>().Last() };
        }
    }
}
