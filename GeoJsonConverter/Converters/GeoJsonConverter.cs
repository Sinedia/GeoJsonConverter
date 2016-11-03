using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sinedia.Json.Converters.WktHelpers;

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
                if (string.IsNullOrEmpty(geoJsonFeatureType))
                {
                    return string.Empty; // We didn't find any
                }

                // Try to parse the found feature type
                FeatureType featureType;
                if (!Enum.TryParse(geoJsonFeatureType, out featureType))
                {
                    return string.Empty; // We couldn't parse it
                }

                // Set the base value for coordinates, needs to be overwritten of we do find point.
                var wktCoordinatesString = "EMPTY";

                // Read the coordinates
                var geoJsonCoordinates = token["coordinates"];
                if (geoJsonCoordinates == null || !geoJsonCoordinates.HasValues)
                {
                    // We couldn't find coordinates or the coordinate string is empty
                    return $"{featureType.ToString().ToUpperInvariant()} {wktCoordinatesString}";
                }

                // Process (convert) the coordinates based on the found feature type
                switch (featureType)
                {
                    case FeatureType.Point:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertPointToWkt(geoJsonCoordinates);
                        break;

                    case FeatureType.LineString:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertLineStringToWkt(geoJsonCoordinates);
                        break;

                    case FeatureType.Polygon:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertPolygonToWkt(geoJsonCoordinates);
                        break;

                    case FeatureType.MultiPoint:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertMultiPointToWkt(geoJsonCoordinates);
                        break;

                    case FeatureType.MultiLineString:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertMultiLineStringToWkt(geoJsonCoordinates);
                        break;

                    case FeatureType.MultiPolygon:
                        wktCoordinatesString = TokenToWktStringConverter.ConvertMultiPolygonToWkt(geoJsonCoordinates);
                        break;
                }

                // Return the complete WKT
                return $"{featureType.ToString().ToUpperInvariant()} {wktCoordinatesString}";
            }
            catch //(Exception exception)
            {
                // Swallow the exception
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
    }
}