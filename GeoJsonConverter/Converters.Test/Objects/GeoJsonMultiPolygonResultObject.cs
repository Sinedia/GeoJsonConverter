using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;

namespace Sinedia.Json.Converters.Test.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class GeoJsonMultiPolygonResultObject
    {
        [JsonProperty("geometry")]
        [JsonConverter(typeof(GeoJsonConverter))]
        public MultiPolygon Geometry { get; set; }
    }
}