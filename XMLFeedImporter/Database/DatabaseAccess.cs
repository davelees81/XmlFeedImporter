using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace XMLFeedImporter.Database
{
    public sealed class DatabaseAccess
    {

        private string _connectionString;


        /// <summary>
        /// Creates a new DatabaseAccess instance with the supplied connectionString.
        /// </summary>
        /// <param name="connectionString">SqlConnection</param>
        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }


        //<summary>
        //Executes a stored procedure, without returning data. i.e for updates and inserts.
        //</summary>
        //<param name="procName">Name of stored procedure to execute.</param>
        //<param name="parameters">Parameters passed to stored procedure.</param>
        public void ExecuteStoredProcedureNonQuery(String procName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procName;
                command.Parameters.AddRange(parameters);

                connection.Open();
                command.ExecuteNonQuery();
            }

        }

    }
}
