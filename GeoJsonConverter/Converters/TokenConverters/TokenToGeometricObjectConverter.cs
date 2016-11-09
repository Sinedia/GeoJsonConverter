using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sinedia.Json.Converters.GeometricObjects;

namespace Sinedia.Json.Converters.TokenConverters
{
    internal class TokenToGeometricObjectConverter : IGeometricObject
    {
        public object Convert(FeatureType featureType, JToken geoJsonCoordinates)
        {
            // Create the result value
            IGeometricObject geometricObject = null;

            // Process (convert) the coordinates based on the found feature type
            switch (featureType)
            {
                case FeatureType.Point:
                    // TODO
                    geometricObject = new Point();

                    break;

                case FeatureType.LineString:
                    // TODO
                    break;

                case FeatureType.Polygon:
                    // TODO
                    break;

                case FeatureType.MultiPoint:
                    // TODO
                    break;

                case FeatureType.MultiLineString:
                    // TODO
                    break;

                case FeatureType.MultiPolygon:
                    // TODO
                    break;
            }

            // Return the geometric object
            return geometricObject;
        }
    }
}
