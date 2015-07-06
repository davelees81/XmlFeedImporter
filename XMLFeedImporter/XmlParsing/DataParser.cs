using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.Concurrent;

namespace XMLFeedImporter.XmlParsing
{
    public sealed class DataParser : IDataParser
    {
        string _rootPath = string.Empty;
        private string _fileName = string.Empty;        
        private List<IDataObtainer> _dataObtainers;
        private BlockingCollection<IData> _dtos = new BlockingCollection<IData>();

        public DataParser(List<IDataObtainer> dataObtainers)
        {
            _dataObtainers = dataObtainers;
        }

        public void ParseData(string filePath)
        {
            var root = XElement.Load(filePath);

            foreach (var dataObtainer in _dataObtainers)
            {
                var dataCollection = dataObtainer.GetData(root);

                foreach (var data in dataCollection)
                {
                    var dto = dataObtainer.GetDto();
                    dto.Hydrate(data);
                    if (dto.HasValidData()) { _dtos.Add(dto); }
                }
            }
        }

        public BlockingCollection<IData> GetDtos()
        {
            return _dtos;
        }
    }
}
