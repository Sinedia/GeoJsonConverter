using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sinedia.Json.Converters.Test.Objects;

namespace Sinedia.Json.Converters.Test
{
    /// <summary>Contains tests where the input is a JSON which contains a GeoJSON (valid or not).</summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GeoJsonConverterJsonWithFeatureToWktTests
    {
        [TestMethod]
        public void GeoJsonConverterWithGeoJsonInsideJson_Should_ReturnValidObjectWithWkt()
        {
            // For this test I used a response object as given by the BRK service of the Dutch Kadaster.
            // It contains a complete response which has a GeoJSON inside. This way we can test that 
            // the converter isn't only a converter for GeoJSON, but can handle itself well inside full 
            // JSON objects (sets the reader at the right position and such).

            const string perceel = @"{""kadastraleGemeentenaam"":""Holten"",""perceelnummerRotatie"":0,""sectie"":""A"",""geometry"":{""type"":""Polygon"",""coordinates"":[[[6.415293083651154,52.30069265565988],[6.415312512022843,52.303606383023016],[6.415307532017772,52.30435227682954],[6.415308183112876,52.30559298217335],[6.415310551567866,52.3056427298673],[6.411494010228915,52.30457916099857],[6.411536815076222,52.30356896227386],[6.410554207330468,52.303354742462886],[6.410535705601563,52.303750729883504],[6.410393686697229,52.30375206709379],[6.410394989552987,52.302350229696366],[6.410388364373079,52.30183149955912],[6.415293083651154,52.30069265565988]]]},""kadastraleGemeentecode"":""HTN03"",""kadastraleGrootte"":128270,""perceelnummer"":902,""_links"":{""self"":{""href"":""https://brk.basisregistraties.overheid.nl/api/v1/perceel/90366163""}}}";

            var result = JsonConvert.DeserializeObject<BrkResponseResult>(perceel);

            Assert.AreEqual(result.Geometry, "POLYGON ((6.41529308365115 52.3006926556599, 6.41531251202284 52.303606383023, 6.41530753201777 52.3043522768295, 6.41530818311288 52.3055929821734, 6.41531055156787 52.3056427298673, 6.41149401022891 52.3045791609986, 6.41153681507622 52.3035689622739, 6.41055420733047 52.3033547424629, 6.41053570560156 52.3037507298835, 6.41039368669723 52.3037520670938, 6.41039498955299 52.3023502296964, 6.41038836437308 52.3018314995591, 6.41529308365115 52.3006926556599))");
        }
    }
}
