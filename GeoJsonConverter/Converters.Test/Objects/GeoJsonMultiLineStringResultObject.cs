using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;

namespace Sinedia.Json.Converters.Test.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class GeoJsonMultiLineStringResultObject
    {
        [JsonProperty("geometry")]
        [JsonConverter(typeof(GeoJsonConverter))]
        public MultiLineString Geometry { get; set; }
    }
}