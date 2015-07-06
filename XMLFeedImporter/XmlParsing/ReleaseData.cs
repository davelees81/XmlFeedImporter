using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace XMLFeedImporter.XmlParsing
{
    public sealed class ReleaseData:IData
    {
        public string ReleaseType { get; set; }
        public string GRid { get; set; }
        public string TitleText { get; set; }
        public string DisplayArtistName { get; set; }
        public string LabelName { get; set; }
        public string StoredProcedure { get { return "Insert_CixImport_Release"; } }
        public SqlParameter[] SqlParameters { get { return _SqlParameters; } }

        private SqlParameter[] _SqlParameters = new SqlParameter[5];

        public void Hydrate(XElement releaseData)
        {

            var relatedReleases = from relatedRelease in releaseData.Element("ReleaseDetailsByTerritory").Elements("RelatedRelease")
                                  select relatedRelease;

            if (relatedReleases != null & relatedReleases.Count<XElement>() == 0)
            {
                if (releaseData.Element("ReleaseType") != null)
                    ReleaseType = releaseData.Element("ReleaseType").Value;

                if (releaseData.Element("ReleaseId") != null && releaseData.Element("ReleaseId").Element("GRid") != null)
                    GRid = releaseData.Element("ReleaseId").Element("GRid").Value;

                if (releaseData.Element("ReferenceTitle") != null && releaseData.Element("ReferenceTitle").Element("TitleText") != null)
                    TitleText = releaseData.Element("ReferenceTitle").Element("TitleText").Value;

                if (releaseData.Element("ReleaseDetailsByTerritory") != null && releaseData.Element("ReleaseDetailsByTerritory").Element("DisplayArtistName") != null)
                    DisplayArtistName = releaseData.Element("ReleaseDetailsByTerritory").Element("DisplayArtistName").Value;

                if (releaseData.Element("ReleaseDetailsByTerritory") != null && releaseData.Element("ReleaseDetailsByTerritory").Element("LabelName") != null)
                    LabelName = releaseData.Element("ReleaseDetailsByTerritory").Element("LabelName").Value;


                _SqlParameters[0] = new SqlParameter("ReleaseType", ReleaseType);
                _SqlParameters[1] = new SqlParameter("GRid", GRid);
                _SqlParameters[2] = new SqlParameter("TitleText", TitleText);
                _SqlParameters[3] = new SqlParameter("DisplayArtistName", DisplayArtistName);
                _SqlParameters[4] = new SqlParameter("LabelName", LabelName);
            }

        }

        public bool HasValidData()
        {
            return !(String.IsNullOrEmpty(ReleaseType) ||
                     String.IsNullOrEmpty(GRid) ||
                     String.IsNullOrEmpty(TitleText) ||
                     String.IsNullOrEmpty(DisplayArtistName) ||
                     String.IsNullOrEmpty(LabelName));
        }

    }
}
