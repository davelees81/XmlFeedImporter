using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace XMLFeedImporter.Tests
{
    [TestFixture]
    public class FileProcessingTests
    {

        [Test]
        public void GIVEN__FilesAreCreatedOnDisk__WHEN__RootFtpFolderIsEnumerated__THEN__FilesToProcessAreDiscovered()
        {
            //Arrange
            string rootPath = @"C:\Users\Dave\Documents\Visual Studio 2013\Projects\XMLFeedImporter\XMLFeedImporter\ftp";
            var rootDirectory = new System.IO.DirectoryInfo(rootPath);
            var subDirectoryFiles = new List<string>();
            bool filesToProcessDiscovered = false;

            //Act
            foreach (var leve2bDirectory in rootDirectory.EnumerateDirectories())
            {
                foreach (var leve3bDirectory in leve2bDirectory.EnumerateDirectories())
                {
                    foreach (var file in leve3bDirectory.EnumerateFiles("*.xml"))
                    {
                        subDirectoryFiles.Add(file.FullName);
                    }
                }
            }

            filesToProcessDiscovered = subDirectoryFiles.Count() > 0;

            //Assert
            Assert.IsTrue(filesToProcessDiscovered);
        }

        [Test]
        public void RunApp()
        {
            var ciXmlIntegration = new CiXmlIntegration();

            ciXmlIntegration.StartProcessing();  
        }

    }
}
