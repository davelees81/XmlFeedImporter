using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLFeedImporter.Database;
using XMLFeedImporter.XmlParsing;
using XMLFeedImporter.ImportProcessing;
using System.IO;

namespace XMLFeedImporter.ImportProcessing
{
    public sealed class ImportProcessor
    {
        private const string _processed = "processed";
        private const string _fileTypesToEnumerate = "*.xml";
        private string _ftpFolderPath = string.Empty;
        private string _processedFolderPath = string.Empty;
        private DatabaseAccess _databaseAccess;
        private ParseManager _parseManager;


        public ImportProcessor(DatabaseAccess databaseAccess, ParseManager parseManager, string ftpFolderPath, string processedFolderPath)
        {
            _databaseAccess = databaseAccess;
            _parseManager = parseManager;
            _ftpFolderPath = ftpFolderPath;
            _processedFolderPath = processedFolderPath;
        }

        public void ProcessImport()
        {
            var itemsToProcess = GetItemsToProcess();

            try
            {
                foreach (var topLevelDirectory in itemsToProcess.Keys)
                {
                    ParseFiles(itemsToProcess[topLevelDirectory]);
                }

                CommitToDatabase();
            }
            finally 
            {
                foreach (var topLevelDirectory in itemsToProcess.Keys)
                {
                    MoveDirectoryToProcessed(topLevelDirectory);
                }
            }

        }

        private Dictionary<DirectoryInfo, List<string>> GetItemsToProcess()
        {
            var rootDirectory = new System.IO.DirectoryInfo(_ftpFolderPath);
            var itemsToProcess = new Dictionary<System.IO.DirectoryInfo, List<string>>();

            foreach (var leve2bDirectory in rootDirectory.EnumerateDirectories())
            {
                if (leve2bDirectory.Name.ToLower() == _processed)
                {
                    continue;
                }

                foreach (var leve3bDirectory in leve2bDirectory.EnumerateDirectories())
                {
                    foreach (var file in leve3bDirectory.EnumerateFiles(_fileTypesToEnumerate))
                    {
                        if (itemsToProcess.ContainsKey(leve2bDirectory))
                        {
                            itemsToProcess[leve2bDirectory].Add(file.FullName);
                        }
                        else
                        {
                            var fileList = new List<string>();
                            fileList.Add(file.FullName);
                            itemsToProcess.Add(leve2bDirectory, fileList);
                        }
                    }
                }

            }

            return itemsToProcess;
        }


        private void ParseFiles(List<string> filePaths)
        {
            Parallel.ForEach(filePaths, filePath =>
            {
                _parseManager.Parse(filePath);
            });
        }


        private void CommitToDatabase()
        {
            Parallel.ForEach(_parseManager.GetDtos(), dto =>
            {
                _databaseAccess.ExecuteStoredProcedureNonQuery(dto.StoredProcedure, dto.SqlParameters);
            });
        }


        private void MoveDirectoryToProcessed(DirectoryInfo directory)
        {
            if (System.IO.Directory.Exists(_processedFolderPath + @"\\" + directory.Name))
                System.IO.Directory.Delete(_processedFolderPath + @"\\" + directory.Name, true);

            directory.MoveTo(_processedFolderPath + @"\\" + directory.Name);
        }


    }
}
