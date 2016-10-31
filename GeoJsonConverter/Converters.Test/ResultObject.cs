using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Sinedia.Json.Converters;

namespace Converters.Test
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class ResultObject
    {
        [JsonProperty("geometry")]
        [JsonConverter(typeof(GeoJsonConverter))]
        public string Geometry { get; set; }
    }
}