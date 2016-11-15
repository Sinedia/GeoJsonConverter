using Newtonsoft.Json.Linq;

namespace Sinedia.Json.Converters.TokenConverters
{
    /// <summary>Defines classes which are responsible for token conversions.</summary>
    internal interface ITokenConverter
    {
        /// <summary>Converts the token accordingly the specified the feature type.</summary>
        /// <param name="featureType">The feature type.</param>
        /// <param name="geoJsonCoordinates">The Geo JSON coordinates.</param>
        /// <returns>An object containing the converted values.</returns>
        object Convert(FeatureType featureType, JToken geoJsonCoordinates);
    }
}
