using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using XMLFeedImporter.XmlParsing;


namespace XMLFeedImporter.Tests
{

    [TestFixture]
    public class XMLTests
    {

        [Test]
        public void GIVEN__File_A10341T0000008CMPA_IsProcessed__WHEN__ParseManagerFinishesParsing__THEN__DataReturnedIsNotNull()
        {
            //Arrange
            string rootPath = @"C:\Users\Dave\Documents\Visual Studio 2013\Projects\XMLFeedImporter\XMLFeedImporter\ftp\A10341T0000008CY44\A10341T0000008CMPA";
            string fileName = "A10341T0000008CMPA.xml";
            var parseManager = GetParseManager(rootPath, fileName);
            bool dataReturnedIsNotNull = false;

            //Act
            parseManager.Parse(string.Format(@"{0}\\{1}", rootPath, fileName));

            ReleaseData data = (ReleaseData)parseManager.GetDtos()[0];
            ReleaseData data2 = (ReleaseData)parseManager.GetDtos()[1];

            if
                (
                    data.DisplayArtistName != string.Empty &&
                    data.GRid != string.Empty &&
                    data.LabelName != string.Empty &&
                    data.ReleaseType != string.Empty &&
                    data.TitleText != string.Empty &&
                
                    data2.DisplayArtistName != string.Empty &&
                    data2.GRid != string.Empty &&
                    data2.LabelName != string.Empty &&
                    data2.ReleaseType != string.Empty &&
                    data2.TitleText != string.Empty
                )
                {
                    dataReturnedIsNotNull = true;
                }

            //Assert
            Assert.IsTrue(dataReturnedIsNotNull);
        }

        private ParseManager GetParseManager(string rootPath, string fileName)
        {
            var dataObtainers = new List<IDataObtainer>();
            dataObtainers.Add(new ReleaseDataObtainer());
            var dataParser = new DataParser(dataObtainers);

            var parsers = new List<IDataParser>();
            parsers.Add(dataParser);
            var parseManager = new ParseManager(parsers);
        
            return parseManager;
        }

    }
 }

