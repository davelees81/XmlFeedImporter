using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLFeedImporter.XmlParsing
{
    public sealed class ReleaseDataObtainer : IDataObtainer
    {
        public IEnumerable<XElement> GetData(XElement rootElement)
        {
            var releases = from release in rootElement.Element("ReleaseList").Elements("Release")
                           select release;

            return releases;
        }

        public IData GetDto()
        {
            return new ReleaseData();
        }
    }
}
