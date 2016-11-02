using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Converters.Test.Objects
{
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class BrkResponseResultLinks
    {
        [JsonProperty("self")]
        public BrkResponseLink Self { get; set; }

        [JsonProperty("source")]
        public BrkResponseLink Source { get; set; }
    }
}