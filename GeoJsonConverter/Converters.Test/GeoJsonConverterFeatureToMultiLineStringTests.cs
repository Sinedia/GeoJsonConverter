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
    public class GeoJsonConverterFeatureToMultiLineStringTests
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

            var result = JsonConvert.DeserializeObject<GeoJsonMultiLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithoutMultiLineStringInInput_Should_ReturnNull()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiLineString"",
                                ""coordinates"": """"
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, null);
        }

        [TestMethod]
        public void GeoJsonConverterWithMultiPointInInput_Should_ReturnValidObjectWithMultiPoint()
        {
            const string perceel = @"{
                              ""geometry"": {
                                ""type"": ""MultiLineString"",
                                ""coordinates"": [[[10, 10], [20, 20], [10, 40]], [[40, 40], [30, 30], [40, 20], [30, 10]]]
                              }
                            }";

            var result = JsonConvert.DeserializeObject<GeoJsonMultiLineStringResultObject>(perceel);

            Assert.AreEqual(result.Geometry, new MultiLineString()
            {
                LineStrings = new List<LineString>()
                {
                    new LineString()
                    {
                        Points = new List<Point>()
                        {
                            new Point() { X = 10, Y = 10 },
                            new Point() { X = 20, Y = 20 },
                            new Point() { X = 10, Y = 40 }
                        }
                    },
                    new LineString()
                    {
                        Points = new List<Point>()
                        {
                            new Point() { X = 40, Y = 40 },
                            new Point() { X = 30, Y = 30 },
                            new Point() { X = 40, Y = 20 },
                            new Point() { X = 30, Y = 10 }
                        }
                    }
                }
            });
        }
    }
}
