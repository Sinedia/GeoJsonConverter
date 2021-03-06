# JsonConverters [![NuGet Version](http://img.shields.io/nuget/v/Sinedia.Json.Converters.svg?style=flat)](https://www.nuget.org/packages/Sinedia.Json.Converters)

The project JsonConverters contains a single converter. This is a converter for Newtonsoft's JSON.NET that can convert GeoJSON to WKT or a geometric object (class derived from IGeometricObject).
It was something that was needed in a project I did for Roxit b.v. and for which I couldn't find any suitable code, so I went ahead and wrote it myself.
After this converter was finished I asked Roxit if we could give this code back to the community and they gracefully agreed. So there we have it: a freely available converter.

The converter itself is able to transform a single GeoJSON Feature (e.g. not a full feature list) of any type (Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon) into a single WKT string or a geometric object (class derived from IGeometricObject) of the same type.

## Installation
The converter can be used by simply downloading the [Sinedia.Json.Converters](https://www.nuget.org/packages/Sinedia.Json.Converters) package from NuGet.

## Usage

The following example gives you a WKT string. If you'd like an object which you can use in code simply replace the string (in ```public string Geometry { get; set; }```) for the the IGeometricObject interface or the object you desire and it will transform the GeoJSON to that if possible.
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
                                ""coordinates"": [
                                                   [
                                                     [35, 10], 
                                                     [45, 45], 
                                                     [15, 40], 
                                                     [10, 20], 
                                                     [35, 10]
                                                   ], 
                                                   [
                                                     [20, 30], 
                                                     [35, 35], 
                                                     [30, 20], 
                                                     [20, 30]
                                                   ]
                                                 ]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonResultObject>(perceel);

            // The result should look like this: 
            // POLYGON ((35 10, 45 45, 15 40, 10 20, 35 10), (20 30, 35 35, 30 20, 20 30))
            return result;
        }
```

The unit tests contain several examples if you need more details.

## vNext

The next version (0.5) will have a converter that will recognize more that just the geometric part of the GeoJSON. 
Version 0.5 should be able to recognize feature collections and return a list of geometric objects or WKT strings, 
whatever you desire.