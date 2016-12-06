using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sinedia.Json.Converters.GeometricObjects;
using Sinedia.Json.Converters.TokenConverters;

namespace Sinedia.Json.Converters
{
    /// <summary>
    /// Converts a GeoJson feature (not a feature lists) to a WKT string.
    /// </summary>
    /// <seealso cref="JsonConverter" />
    /// <seealso href="http://geojson.org/geojson-spec.html"/>
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
                var featureType = RetrieveFeatureTypeFromToken(token);

                // If we didn't found or we can't parse what we found any we can't continue, 
                // because we know how to parse the coordinates
                if (featureType == null)
                {
                    // We didn't find any
                    if (objectType == typeof(string))
                    {
                        return string.Empty;
                    }

                    return null;
                }

                // Read the coordinates
                var geoJsonCoordinates = RetrieveJsonCoordinatesFromToken(token);

                // Create the correct return value and return it
                if (objectType == typeof(string))
                {
                    if (geoJsonCoordinates == null || !geoJsonCoordinates.HasValues)
                    {
                        return $"{featureType.ToString().ToUpperInvariant()} EMPTY";
                    }

                    var converter = new TokenToWktConverter();
                    return converter.Convert(featureType.Value, geoJsonCoordinates);
                }

                if (objectType.GetInterfaces().Contains(typeof(IGeometricObject)))
                {
                    if (geoJsonCoordinates == null || !geoJsonCoordinates.HasValues)
                    {
                        return null;
                    }

                    var converter = new TokenToGeometricObjectConverter();
                    return converter.Convert(featureType.Value, geoJsonCoordinates);
                }

                return null;
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
            return
                objectType == typeof(string) ||
                objectType == typeof(IGeometricObject) ||
                objectType == typeof(Point) ||
                objectType == typeof(LineString) ||
                objectType == typeof(Polygon) ||
                objectType == typeof(MultiPoint) ||
                objectType == typeof(MultiLineString) ||
                objectType == typeof(MultiPolygon);
        }

        /// <summary>Tries to retrieve the feature type from a token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>Returns the found feature type or else <c>null</c>.</returns>
        private static FeatureType? RetrieveFeatureTypeFromToken(JToken token)
        {
            // Read the feature value
            var geoJsonFeatureType = (string)token["type"];

            // If we didn't found or we can't parse what we found any we can't continue, 
            // because we know how to parse the coordinates
            FeatureType featureType;
            if (!Enum.TryParse(geoJsonFeatureType, out featureType))
            {
                return null;
            }

            return featureType;
        }

        /// <summary>Tries to retrieve the json coordinates from a token.</summary>
        /// <param name="token">The token.</param>
        /// <returns>Returns the found coordinates or else <c>null</c>.</returns>
        private static JToken RetrieveJsonCoordinatesFromToken(JToken token)
        {
            // Read the feature value
            var geoJsonCoordinates = token["coordinates"];

            if (geoJsonCoordinates == null || !geoJsonCoordinates.HasValues)
            {
                return null;
            }

            return geoJsonCoordinates;
        }
    }
}