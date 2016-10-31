using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Converters.Test
{
    [TestClass]
    public class GeoJsonConverterTests
    {
        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithEmptyInput_Should_ReturnValidObjectWithEmptyWktString()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": """",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, string.Empty);
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithoutPointInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Point"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "POINT EMPTY");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithPointInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Point"",
                                ""coordinates"": [30, 10]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "POINT (30 10)");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithLineStringInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""LineString"",
                                ""coordinates"": [[30, 10], [10, 30], [40, 40]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "LINESTRING (30 10, 10 30, 40 40)");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithSimplePolygonInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": [[[30, 10], [40, 40], [20, 40], [10, 20], [30, 10]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithNotSoSimplePolygonInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": [[[35, 10], [45, 45], [15, 40], [10, 20], [35, 10]], [[20, 30], [35, 35], [30, 20], [20, 30]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "POLYGON ((35 10, 45 45, 15 40, 10 20, 35 10), (20 30, 35 35, 30 20, 20 30))");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithMultiPointInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPoint"",
                                ""coordinates"": [[10, 40], [40, 30], [20, 20], [30, 10]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))");
            //Assert.AreEqual(result.Geometry, "MULTIPOINT (10 40, 40 30, 20 20, 30 10)");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithLineMultiStringInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiLineString"",
                                ""coordinates"": [[[10, 10], [20, 20], [10, 40]], [[40, 40], [30, 30], [40, 20], [30, 10]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "MULTILINESTRING ((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10))");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithSimpleMultiPolygonInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPolygon"",
                                ""coordinates"": [[[[30, 20], [45, 40], [10, 40], [30, 20]]], [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))");
        }

        [TestMethod]
        public void GeoJsonFeatureToWktConverterWithNotSoSimpleMultiPolygonInInput_Should_ReturnValidObjectWithWkt()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPolygon"",
                                ""coordinates"": [[[[40, 40], [20, 45], [45, 30], [40, 40]]], [[[20, 35], [10, 30], [10, 10], [30, 5], [45, 20], [20, 35]], [[30, 20], [20, 15], [20, 25], [30, 20]]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<ResultObject>(perceel);

            Assert.AreEqual(result.Geometry, "MULTIPOLYGON (((40 40, 20 45, 45 30, 40 40)), ((20 35, 10 30, 10 10, 30 5, 45 20, 20 35), (30 20, 20 15, 20 25, 30 20)))");
        }
    }
}
