using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Sinedia.Json.Converters.Test.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class BrkResponseResult
    {
        [JsonProperty("kadastraleGemeentenaam")]
        public string KadastraleGemeentenaam { get; set; }

        [JsonProperty("kadastraleGemeentecode")]
        public string KadastraleGemeentecode { get; set; }

        [JsonProperty("perceelnummer")]
        public int Perceelnummer { get; set; }

        [JsonProperty("sectie")]
        public string Sectie { get; set; }

        [JsonProperty("perceelnummerRotatie")]
        public double PerceelnummerRotatie { get; set; }

        [JsonProperty("kadastraleGrootte")]
        public int KadastraleGrootte { get; set; }

        [JsonProperty("geometry")]
        [JsonConverter(typeof(GeoJsonConverter))]
        public string Geometry { get; set; }

        [JsonProperty("_links")]
        public BrkResponseResultLinks Links { get; set; }
    }
}