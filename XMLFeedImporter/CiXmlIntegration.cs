using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using XMLFeedImporter.Database;
using XMLFeedImporter.XmlParsing;
using XMLFeedImporter.ImportProcessing;

namespace XMLFeedImporter
{
    public sealed class CiXmlIntegration
    {
        public void StartProcessing()
        {
            string ftpPath = Properties.Settings.Default.FtpFolderPath;
            string processedPath = Properties.Settings.Default.ProcessedFolderPath;

            var databaseAccess = new DatabaseAccess(Properties.Settings.Default.DatabaseConnectionString);
            var parseManager = GetParseManager();
            
            var importProcessor = new ImportProcessor(databaseAccess, parseManager, ftpPath, processedPath);
            importProcessor.ProcessImport();
        }

        private ParseManager GetParseManager()
        {
            var parsers = GetDataParsers();
            var parseManager = new ParseManager(parsers);

            return parseManager;
        }

        private List<IDataParser> GetDataParsers()
        {
            var dataObtainers = new List<IDataObtainer>();
            dataObtainers.Add(new ReleaseDataObtainer());

            var dataParser = new DataParser(dataObtainers);

            var parsers = new List<IDataParser>();
            parsers.Add(dataParser);

            return parsers;
        }
    }
}
