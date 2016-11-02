# JsonConverters

The project JsonConverters contains a single converter. This is a converter for Newtonsoft's JSON.NET that can convert GeoJSON to WKT.
It was something that was needed in a project I did for Roxit b.v. and for which I couldn't find any suitable code, so I went ahead and wrote it myself.
After this converter was finished I asked Roxit if we could give this code back to the community and they gracefully agreed. So there we have it: a freely available converter.

The converter itself is able to transform a single GeoJSON Feature (e.g. not a full feature list) of any type (Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon) into a single WKT string.

### Installation
The converter can be used by simply downloading the package from [NuGet](docshttps://www.nuget.org/packages/Sinedia.Json.Converters).

### Usage

```
    [JsonObject(MemberSerialization.OptIn)]
    [ExcludeFromCodeCoverage]
    public class GeoJsonResultObject
    {
        [JsonProperty("geometry")]
        [JsonConverter(typeof(GeoJsonConverter))]
        public string Geometry { get; set; }
    }

    public string GeoJsonFeatureToWktConverterExample()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": [[[35, 10], [45, 45], [15, 40], [10, 20], [35, 10]], [[20, 30], [35, 35], [30, 20], [20, 30]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonResultObject>(perceel);

            //"POLYGON ((35 10, 45 45, 15 40, 10 20, 35 10), (20 30, 35 35, 30 20, 20 30))"
            return result;
        }
```

The unit tests contain several examples if you need more details.