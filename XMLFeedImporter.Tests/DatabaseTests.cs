using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using XMLFeedImporter.Database;
using System.Data.SqlClient;

namespace XMLFeedImporter.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private const string _connectionString = @"Server=localhost\SQLEXPRESS;Database=SonStream;Trusted_Connection=Yes;";

        [Test]
        public void GIVEN__StoredProcedureTestProcIsCalled__WHEN__DatabaseAccessIsUsedWithMultipleThreads__THEN__ExceptionIsNotThrown()
        {
            //Arrange
            var databaseAccess = new DatabaseAccess(_connectionString);
            bool exceptionThrown = false;

            //Act
            try
            {
                Parallel.For(0, 1000, i =>
                                            {
                                                databaseAccess.ExecuteStoredProcedureNonQuery("TestProc", new SqlParameter("Id", i));
                                            }
                            );
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            //Assert
            Assert.False(exceptionThrown);
        }

    }
}
