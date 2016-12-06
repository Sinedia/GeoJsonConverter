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
    public class GeoJsonConverterFeatureToMultiPolygonTests
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

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutMultiPolygonInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPolygon"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithMultiPolygon1InInput_Should_ReturnValidObjectWithMultiPolygon()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPolygon"",
                                ""coordinates"": [[[[30, 20], [45, 40], [10, 40], [30, 20]]], [[[15, 5], [40, 10], [10, 20], [5, 10], [15, 5]]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new MultiPolygon()
            {
                Polygons = new List<Polygon>()
                {
                    new Polygon()
                    {
                        LineSegments = new List<LineString>()
                        {
                            new LineString()
                            {
                                Points = new List<Point>()
                                {
                                    new Point() { X = 30, Y = 20 },
                                    new Point() { X = 45, Y = 40 },
                                    new Point() { X = 10, Y = 40 },
                                    new Point() { X = 30, Y = 20 }
                                }
                            }
                        }
                    },
                    new Polygon()
                    {
                        LineSegments = new List<LineString>()
                        {
                            new LineString()
                            {
                                Points = new List<Point>()
                                {
                                    new Point() { X = 15, Y = 5 },
                                    new Point() { X = 40, Y = 10 },
                                    new Point() { X = 10, Y = 20 },
                                    new Point() { X = 5, Y = 10 },
                                    new Point() { X = 15, Y = 5 }
                                }
                            }
                        }
                    }
                }
            });
        }

        [TestMethod]
        public void GeoJsonConverterWithMultiPolygon2InInput_Should_ReturnValidObjectWithMultiPolygon()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiPolygon"",
                                ""coordinates"": [[[[40, 40], [20, 45], [45, 30], [40, 40]]], [[[20, 35], [10, 30], [10, 10], [30, 5], [45, 20], [20, 35]], [[30, 20], [20, 15], [20, 25], [30, 20]]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiPolygonResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new MultiPolygon()
            {
                Polygons = new List<Polygon>()
                {
                    new Polygon()
                    {
                        LineSegments = new List<LineString>()
                        {
                            new LineString()
                            {
                                Points = new List<Point>()
                                {
                                    new Point() { X = 40, Y = 40 },
                                    new Point() { X = 20, Y = 45 },
                                    new Point() { X = 45, Y = 30 },
                                    new Point() { X = 40, Y = 40 }
                                }
                            }
                        }
                    },
                    new Polygon()
                    {
                        LineSegments = new List<LineString>()
                        {
                            new LineString()
                            {
                                Points = new List<Point>()
                                {
                                    new Point() { X = 20, Y = 35 },
                                    new Point() { X = 10, Y = 30 },
                                    new Point() { X = 10, Y = 10 },
                                    new Point() { X = 30, Y = 5 },
                                    new Point() { X = 45, Y = 20 },
                                    new Point() { X = 20, Y = 35 }
                                }
                            },
                            new LineString()
                            {
                                Points = new List<Point>()
                                {
                                    new Point() { X = 30, Y = 20 },
                                    new Point() { X = 20, Y = 15 },
                                    new Point() { X = 20, Y = 25 },
                                    new Point() { X = 30, Y = 20 }
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
