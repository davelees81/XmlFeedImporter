using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XMLFeedImporter.XmlParsing
{
    public interface IData
    {
        void Hydrate(XElement data);

        string StoredProcedure { get; }

        System.Data.SqlClient.SqlParameter[] SqlParameters { get; }

        bool HasValidData();
    }
}
