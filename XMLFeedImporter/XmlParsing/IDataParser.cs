using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace XMLFeedImporter.XmlParsing
{
    public interface IDataParser
    {
        void ParseData(string filePath);
        BlockingCollection<IData> GetDtos();
    }
}
