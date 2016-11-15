using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;
using Sinedia.Json.Converters.Test.Objects;

namespace Sinedia.Json.Converters.Test
{
    [TestClass]
    public class GeoJsonConverterFeatureToLineStringTests
    {
        [TestMethod]
        public void GeoJsonConverterWithEmptyInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": """",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutLineStringInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""LineString"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithLineStringInInput_Should_ReturnValidObjectWithLineString()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""LineString"",
                                ""coordinates"": [
                                                   [30, 10], [10, 30], [40, 40]
                                                 ]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new LineString() { Points = new List<Point>() { new Point() { X = 30, Y = 10 }, new Point() { X = 10, Y = 30 }, new Point() { X = 40, Y = 40 } } });
        }
    }
}
