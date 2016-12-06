using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sinedia.Json.Converters.GeometricObjects;
using Sinedia.Json.Converters.Test.Objects;

namespace Sinedia.Json.Converters.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GeoJsonConverterFeatureToPolygonTests
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

            var result = JsonConvert.DeserializeObject<GeoJsonPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutPolygonInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithPolygon1InInput_Should_ReturnValidObjectWithPolygon()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": [[[30, 10], [40, 40], [20, 40], [10, 20], [30, 10]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new Polygon()
            {
                LineSegments = new List<LineString>()
                {
                    new LineString()
                    {
                        Points = new List<Point>()
                        {
                            new Point() { X = 30, Y = 10 },
                            new Point() { X = 40, Y = 40 },
                            new Point() { X = 20, Y = 40 },
                            new Point() { X = 10, Y = 20 },
                            new Point() { X = 30, Y = 10 }
                        }
                    }
                }
            });
        }

        [TestMethod]
        public void GeoJsonConverterWithPolygon2InInput_Should_ReturnValidObjectWithPolygon()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""Polygon"",
                                ""coordinates"": [[[35, 10], [45, 45], [15, 40], [10, 20], [35, 10]], [[20, 30], [35, 35], [30, 20], [20, 30]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new Polygon()
            {
                LineSegments = new List<LineString>()
                {
                    new LineString()
                    {
                        Points = new List<Point>()
                        {
                            new Point() { X = 35, Y = 10 },
                            new Point() { X = 45, Y = 45 },
                            new Point() { X = 15, Y = 40 },
                            new Point() { X = 10, Y = 20 },
                            new Point() { X = 35, Y = 10 }
                        }
                    },
                    new LineString()
                    {
                        Points = new List<Point>()
                        {
                            new Point() { X = 20, Y = 30 },
                            new Point() { X = 35, Y = 35 },
                            new Point() { X = 30, Y = 20 },
                            new Point() { X = 20, Y = 30 },
                        }
                    }
                }
            });
        }
    }
}
