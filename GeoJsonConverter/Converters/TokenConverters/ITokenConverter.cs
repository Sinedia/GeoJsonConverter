using Newtonsoft.Json.Linq;

namespace Sinedia.Json.Converters.TokenConverters
{
    internal interface ITokenConverter
    {
        string Convert(FeatureType featureType, JToken geoJsonCoordinates);
    }
}
