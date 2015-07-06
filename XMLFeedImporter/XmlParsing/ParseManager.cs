using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLFeedImporter.XmlParsing
{
    public sealed class ParseManager
    {
        private List<IDataParser> _parsers = new List<IDataParser>();

        public ParseManager(List<IDataParser> parsers)
        {
            _parsers = parsers;
        }

        public void Parse(string filePath)
        {
            foreach (var parser in _parsers)
            {
                parser.ParseData(filePath);
            }
        }

        public List<IData> GetDtos()
        {
            var dtos = new List<IData>();
            
            foreach (var parser in _parsers)
            {
                dtos.AddRange(parser.GetDtos());
            }

            return dtos;
        }

    }
}
