using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Sinedia.Json.Converters.TokenConverters
{
    internal class TokenToWktConverter : ITokenConverter
    {
        public string Convert(FeatureType featureType, JToken geoJsonCoordinates)
        {
            // Set the base value for the result values.
            var wktCoordinatesString = "EMPTY";

            // Process (convert) the coordinates based on the found feature type
            switch (featureType)
            {
                case FeatureType.Point:
                    wktCoordinatesString = ConvertTokenToPoint(geoJsonCoordinates);

                    break;

                case FeatureType.LineString:
                    wktCoordinatesString = ConvertTokenToLineString(geoJsonCoordinates);
                    break;

                case FeatureType.Polygon:
                    wktCoordinatesString = ConvertTokenToPolygon(geoJsonCoordinates);
                    break;

                case FeatureType.MultiPoint:
                    wktCoordinatesString = ConvertTokenToMultiPoint(geoJsonCoordinates);
                    break;

                case FeatureType.MultiLineString:
                    wktCoordinatesString = ConvertTokenToMultiLineString(geoJsonCoordinates);
                    break;

                case FeatureType.MultiPolygon:
                    wktCoordinatesString = ConvertTokenToMultiPolygon(geoJsonCoordinates);
                    break;
            }

            // Return the complete WKT
            return $"{featureType.ToString().ToUpperInvariant()} {wktCoordinatesString}";
        }

        /// <summary>Converts a list of points (x and y coordinate).</summary>
        /// <param name="multiPoint">The representation of a set of points.</param>
        /// <returns>A WKT representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">multiPoint</exception>
        private static string ConvertTokenToMultiPoint(JToken multiPoint)
        {
            if (multiPoint == null) throw new ArgumentNullException(nameof(multiPoint));

            var wktMultiPoint = string.Empty;

            foreach (var point in multiPoint)
            {
                if (!string.IsNullOrEmpty(wktMultiPoint))
                {
                    wktMultiPoint += ", ";
                }

                wktMultiPoint += ConvertTokenToPoint(point);
            }

            return $"({wktMultiPoint.Trim()})";
        }

        /// <summary>Converts a list of LineStrings.</summary>
        /// <param name="multiLineString">The representation of a MultiLineString.</param>
        /// <returns>A WKT representation of the MultiLineString.</returns>
        /// <exception cref="ArgumentNullException">multiLineString</exception>
        private static string ConvertTokenToMultiLineString(JToken multiLineString)
        {
            if (multiLineString == null) throw new ArgumentNullException(nameof(multiLineString));

            var wktMultiLineString = string.Empty;

            foreach (var lineString in multiLineString)
            {
                if (!string.IsNullOrEmpty(wktMultiLineString))
                {
                    wktMultiLineString += ", ";
                }

                wktMultiLineString += ConvertTokenToLineString(lineString);
            }

            return $"({wktMultiLineString.Trim()})";
        }

        /// <summary>Converts a list of Polygons.</summary>
        /// <param name="multiPolygon">The representation of a MultiPolygon.</param>
        /// <returns>A WKT representation of the MultiPolygon.</returns>
        /// <exception cref="ArgumentNullException">multiPolygon</exception>
        private static string ConvertTokenToMultiPolygon(JToken multiPolygon)
        {
            if (multiPolygon == null) throw new ArgumentNullException(nameof(multiPolygon));

            var wktMultiPolygon = string.Empty;

            foreach (var polygon in multiPolygon)
            {
                if (!string.IsNullOrEmpty(wktMultiPolygon))
                {
                    wktMultiPolygon += ", ";
                }

                wktMultiPolygon += ConvertTokenToPolygon(polygon);
            }

            return $"({wktMultiPolygon.Trim()})";
        }

        /// <summary>Converts a polygon with its shapes and coordinates.</summary>
        /// <param name="polygon">The representation of a polygon (with a list of shapes with a list of coordinates).</param>
        /// <returns>A WKT representation of the polygon.</returns>
        /// <exception cref="ArgumentNullException">polygon</exception>
        private static string ConvertTokenToPolygon(JToken polygon)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));

            var wktPolygon = string.Empty;

            foreach (var shape in polygon)
            {
                if (!string.IsNullOrEmpty(wktPolygon))
                {
                    wktPolygon += ", ";
                }

                wktPolygon += ConvertTokenToLineString(shape);
            }

            return $"({wktPolygon})";
        }

        /// <summary>Converts a LineString (or a shape inside a polygon) with a list of coordinates.</summary>
        /// <param name="lineString">The representation of a LineString (with a list of coordinates).</param>
        /// <returns>A WKT representation of the LineString.</returns>
        /// <exception cref="ArgumentNullException">lineString</exception>
        private static string ConvertTokenToLineString(JToken lineString)
        {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));

            var wktLineString = string.Empty;

            foreach (var coordinateList in lineString)
            {
                if (!string.IsNullOrEmpty(wktLineString))
                {
                    wktLineString += ", ";
                }

                wktLineString += $"{ConvertTokenToCoordinates(coordinateList)}";
            }

            return $"({wktLineString})";
        }

        /// <summary>Converts a point.</summary>
        /// <param name="point">The representation of a point.</param>
        /// <returns>A WKT representation of the same point.</returns>
        /// <exception cref="ArgumentNullException">point</exception>
        private static string ConvertTokenToPoint(JToken point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));

            return $"({ConvertTokenToCoordinates(point)})";
        }

        /// <summary>Converts a list of coordinates (x and y coordinate).</summary>
        /// <param name="coordinates">The representation of a set of coordinates.</param>
        /// <returns>A WKT coordinate representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">coordinateList</exception>
        /// <exception cref="ArgumentException">There always need to be an x and y coordinate. - coordinateList</exception>
        private static string ConvertTokenToCoordinates(JToken coordinates)
        {
            if (coordinates == null) throw new ArgumentNullException(nameof(coordinates));
            if (coordinates.Count() != 2) throw new ArgumentException(@"There always need to be an x and y coordinate.", nameof(coordinates));

            var wktCoordinates = string.Empty;

            foreach (var coordinate in coordinates.Values<double>())
            {
                wktCoordinates += $"{coordinate.ToString(CultureInfo.InvariantCulture)} ";
            }

            return wktCoordinates.Trim();
        }
    }
}
