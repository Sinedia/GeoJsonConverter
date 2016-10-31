using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sinedia.Json.Converters
{
    /// <summary>
    /// Converts a GeoJson feature (not a feature lists) to a WKT string.
    /// </summary>
    /// <seealso cref="JsonConverter" />
    /// <seealso href="https://en.wikipedia.org/wiki/GeoJSON"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Well-known_text"/>
    public class GeoJsonConverter : JsonConverter
    {
        /// <summary> Gets a value indicating whether this <see cref="JsonConverter" /> can read JSON. </summary>
        /// <value><c>true</c>; since this converter can read JSON.</value>
        public override bool CanRead => true;

        /// <summary> Gets a value indicating whether this <see cref="JsonConverter" /> can write JSON. </summary>
        /// <value><c>false</c>; since this converter can't write JSON.</value>
        public override bool CanWrite => false;

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException">Unnecessary because CanWrite is false. The type will skip the converter.</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The found value if any.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                var token = JToken.Load(reader);

                // Read the feature value
                var geoJsonFeatureType = (string)token["type"];

                // If we found any we can continue, because we know how to parse the feature
                if (!string.IsNullOrEmpty(geoJsonFeatureType))
                {
                    // Set the base value for coordinates, needs to be overwritten of we do find point.
                    var wktCoordinatesString = "EMPTY";

                    FeatureType featureType;
                    if (Enum.TryParse(geoJsonFeatureType, out featureType))
                    {
                        var geoJsonCoordinates = token["coordinates"];

                        if (geoJsonCoordinates != null && geoJsonCoordinates.HasValues)
                        {
                            switch (featureType)
                            {
                                case FeatureType.Point:
                                    wktCoordinatesString = ConvertPointToWkt(geoJsonCoordinates);
                                    break;

                                case FeatureType.LineString:
                                    wktCoordinatesString = ConvertLineStringToWkt(geoJsonCoordinates);
                                    break;

                                case FeatureType.Polygon:
                                    wktCoordinatesString = ConvertPolygonToWkt(geoJsonCoordinates);
                                    break;

                                case FeatureType.MultiPoint:
                                    wktCoordinatesString = ConvertMultiPointToWkt(geoJsonCoordinates);
                                    break;

                                case FeatureType.MultiLineString:
                                    wktCoordinatesString = ConvertMultiLineStringToWkt(geoJsonCoordinates);
                                    break;

                                case FeatureType.MultiPolygon:
                                    wktCoordinatesString = ConvertMultiPolygonToWkt(geoJsonCoordinates);
                                    break;
                            }
                        }

                        return $"{featureType.ToString().ToUpperInvariant()} {wktCoordinatesString}";
                    }
                }

                return string.Empty;
            }
            catch //(Exception exception)
            {
                // Swallow;
                return string.Empty;
            }
        }

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        /// <summary>Converts a list of points (x and y coordinate).</summary>
        /// <param name="multiPoint">The representation of a set of points.</param>
        /// <returns>A WKT representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">multiPoint</exception>
        private static string ConvertMultiPointToWkt(JToken multiPoint)
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
        private static string ConvertMultiLineStringToWkt(JToken multiLineString)
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
        private static string ConvertMultiPolygonToWkt(JToken multiPolygon)
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
        private static string ConvertPolygonToWkt(JToken polygon)
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
        private static string ConvertLineStringToWkt(JToken lineString)
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
        private static string ConvertPointToWkt(JToken point)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));

            return $"({ConvertCoordinatesToWkt(point)})";
        }

        /// <summary>Converts a list of coordinates (x and y coordinate).</summary>
        /// <param name="coordinates">The representation of a set of coordinates.</param>
        /// <returns>A WKT coordinate representation of the same list.</returns>
        /// <exception cref="ArgumentNullException">coordinateList</exception>
        /// <exception cref="ArgumentException">There always need to be an x and y coordinate. - coordinateList</exception>
        private static string ConvertCoordinatesToWkt(JToken coordinates)
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