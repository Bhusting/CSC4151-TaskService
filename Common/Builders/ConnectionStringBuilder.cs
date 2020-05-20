using System;
using System.Collections.Generic;
using System.Text;
using Common.Settings;

namespace Common.Builders
{
    public static class ConnectionStringBuilder
    {
        /// <summary>
        /// Builds the Connection String to the SQL Table
        /// </summary>
        /// <param name="sqlSettings">The Settings for the SQL Connection</param>
        /// <returns>Connection String</returns>
        public static string BuildSQLConnectionString(SqlSettings sqlSettings)
        {
            return $"Server=tcp:{sqlSettings.Server},1433;Initial Catalog={sqlSettings.Catalog};Persist Security Info=False;User ID={sqlSettings.User};Password={sqlSettings.Password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

    }
}