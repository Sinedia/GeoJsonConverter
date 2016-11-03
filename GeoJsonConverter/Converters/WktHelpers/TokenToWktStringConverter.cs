using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Sinedia.Json.Converters.WktHelpers
{
    internal static class TokenToWktStringConverter
    {
        /// <summary>Converts a list of points (x and y coordinate).</summary>
        /// <param name="multiPoint">The representation of a set of points.</param>
        /// <returns>A WKT representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">multiPoint</exception>
        public static string ConvertMultiPointToWkt(JToken multiPoint)
        {
            if (multiPoint == null) throw new ArgumentNullException(nameof(multiPoint));

            var wktMultiPoint = string.Empty;

            foreach (var point in multiPoint)
            {
                if (!string.IsNullOrEmpty(wktMultiPoint))
                {
                    wktMultiPoint += ", ";
                }

                wktMultiPoint += ConvertPointToWkt(point);
            }

            return $"({wktMultiPoint.Trim()})";
        }

        /// <summary>Converts a list of LineStrings.</summary>
        /// <param name="multiLineString">The representation of a MultiLineString.</param>
        /// <returns>A WKT representation of the MultiLineString.</returns>
        /// <exception cref="ArgumentNullException">multiLineString</exception>
        public static string ConvertMultiLineStringToWkt(JToken multiLineString)
        {
            if (multiLineString == null) throw new ArgumentNullException(nameof(multiLineString));

            var wktMultiLineString = string.Empty;

            foreach (var lineString in multiLineString)
            {
                if (!string.IsNullOrEmpty(wktMultiLineString))
                {
                    wktMultiLineString += ", ";
                }

                wktMultiLineString += ConvertLineStringToWkt(lineString);
            }

            return $"({wktMultiLineString.Trim()})";
        }

        /// <summary>Converts a list of Polygons.</summary>
        /// <param name="multiPolygon">The representation of a MultiPolygon.</param>
        /// <returns>A WKT representation of the MultiPolygon.</returns>
        /// <exception cref="ArgumentNullException">multiPolygon</exception>
        public static string ConvertMultiPolygonToWkt(JToken multiPolygon)
        {
            if (multiPolygon == null) throw new ArgumentNullException(nameof(multiPolygon));

            var wktMultiPolygon = string.Empty;

            foreach (var polygon in multiPolygon)
            {
                if (!string.IsNullOrEmpty(wktMultiPolygon))
                {
                    wktMultiPolygon += ", ";
                }

                wktMultiPolygon += ConvertPolygonToWkt(polygon);
            }

            return $"({wktMultiPolygon.Trim()})";
        }

        /// <summary>Converts a polygon with its shapes and coordinates.</summary>
        /// <param name="polygon">The representation of a polygon (with a list of shapes with a list of coordinates).</param>
        /// <returns>A WKT representation of the polygon.</returns>
        /// <exception cref="ArgumentNullException">polygon</exception>
        public static string ConvertPolygonToWkt(JToken polygon)
        {
            if (polygon == null) throw new ArgumentNullException(nameof(polygon));

            var wktPolygon = string.Empty;

            foreach (var shape in polygon)
            {
                if (!string.IsNullOrEmpty(wktPolygon))
                {
                    wktPolygon += ", ";
                }

                wktPolygon += ConvertLineStringToWkt(shape);
            }

            return $"({wktPolygon})";
        }

        /// <summary>Converts a LineString (or a shape inside a polygon) with a list of coordinates.</summary>
        /// <param name="lineString">The representation of a LineString (with a list of coordinates).</param>
        /// <returns>A WKT representation of the LineString.</returns>
        /// <exception cref="ArgumentNullException">lineString</exception>
        public static string ConvertLineStringToWkt(JToken lineString)
        {
            if (lineString == null) throw new ArgumentNullException(nameof(lineString));

            var wktLineString = string.Empty;

            foreach (var coordinateList in lineString)
            {
                if (!string.IsNullOrEmpty(wktLineString))
                {
                    wktLineString += ", ";
                }

                wktLineString += $"{ConvertCoordinatesToWkt(coordinateList)}";
            }

            return $"({wktLineString})";
        }

        /// <summary>Converts a point.</summary>
        /// <param name="point">The representation of a point.</param>
        /// <returns>A WKT representation of the same point.</returns>
        /// <exception cref="ArgumentNullException">point</exception>
        public static string ConvertPointToWkt(JToken point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));

            return $"({ConvertCoordinatesToWkt(point)})";
        }

        /// <summary>Converts a list of coordinates (x and y coordinate).</summary>
        /// <param name="coordinates">The representation of a set of coordinates.</param>
        /// <returns>A WKT coordinate representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">coordinateList</exception>
        /// <exception cref="ArgumentException">There always need to be an x and y coordinate. - coordinateList</exception>
        public static string ConvertCoordinatesToWkt(JToken coordinates)
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
