using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Sinedia.Json.Converters.Test.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class BrkResponseLink
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}