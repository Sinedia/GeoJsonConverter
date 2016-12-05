using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;
using Sinedia.Json.Converters.Test.Objects;

namespace Sinedia.Json.Converters.Test
{
    [TestClass]
    public class GeoJsonConverterFeatureToMultiPointTests
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

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutMultiPointInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPoint"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithMultiPointInInput_Should_ReturnValidObjectWithPolygon()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPoint"",
                                ""coordinates"": [[10, 40], [40, 30], [20, 20], [30, 10]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new MultiPoint()
            {
                Points = new List<Point>()
                {
                    new Point() { X = 10, Y = 40 },
                    new Point() { X = 40, Y = 30 },
                    new Point() { X = 20, Y = 20 },
                    new Point() { X = 30, Y = 10 }
                }
            });
        }
    }
}
