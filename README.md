# JsonConverters

The project JsonConverters contains a single converter. This is a converter for Newtonsoft's JSON.NET that can convert GeoJSON to WKT.
It was something that was needed in a project I did for Roxit b.v. and for which I couldn't find any suitable code, so I went ahead and wrote it myself.
After this converter was finished I asked Roxit if we could give this code back to the community and they gracefully agreed. So there we have it: a freely available converter.

The converter itself is able to transform a single GeoJSON Feature (e.g. not a full feature list) of any type (Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon) into a single WKT string.
