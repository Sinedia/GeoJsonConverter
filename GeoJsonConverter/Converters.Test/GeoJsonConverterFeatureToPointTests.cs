using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;
using Sinedia.Json.Converters.Test.Objects;

namespace Sinedia.Json.Converters.Test
{
    [TestClass]
    public class GeoJsonConverterFeatureToPointTests
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

            var result = JsonConvert.DeserializeObject<GeoJsonPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutPointInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Point"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithPointInInput_Should_ReturnValidObjectWithPoint()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Point"",
                                ""coordinates"": [30, 10]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonPointResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new Point() { X = 30, Y = 10 });
        }
    }
}
