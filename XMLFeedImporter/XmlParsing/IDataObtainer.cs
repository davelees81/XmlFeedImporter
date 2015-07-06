using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLFeedImporter.XmlParsing
{
    public interface IDataObtainer
    {
       IEnumerable<XElement> GetData(XElement rootElement);
       IData GetDto();
    }
}
