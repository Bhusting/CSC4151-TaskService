using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Common.Builders;
using Common.Settings;

namespace Common.Clients
{
    public class SqlClient
    {
        private readonly SqlSettings _sqlSettings;

        public SqlClient(SqlSettings sqlSettings)
        {
            _sqlSettings = sqlSettings;
        }

        public async Task<List<T>> Get<T>(string cmd)
        {
            var objList = new List<T>();
            using (var sqlConnection = new SqlConnection(ConnectionStringBuilder.BuildSQLConnectionString(_sqlSettings)))
            {
                await sqlConnection.OpenAsync();
                using (var sqlCmd = new SqlCommand(cmd, sqlConnection))
                {
                    var reader = await sqlCmd.ExecuteReaderAsync();
                    var schema = reader.GetSchemaTable();

                    string[] columnNames = new string[schema.Rows.Count];

                    for (int i = 0; i < schema.Rows.Count; i++)
                        columnNames[i] = schema.Rows[i][0].ToString();

                    while (await reader.ReadAsync())
                    {
                        var obj = Activator.CreateInstance<T>();
                        var props = obj.GetType().GetProperties();
                        foreach (var columnName in columnNames)
                        {

                            foreach (var prop in props)
                            {
                                if (columnName.ToUpper() == prop.Name.ToUpper())
                                {
                                    if (prop.PropertyType == typeof(Guid))
                                    {
                                        prop.SetValue(obj, new Guid(reader[columnName].ToString()));
                                        break;

                                    }
                                    else if (prop.PropertyType == typeof(DateTime))
                                    {
                                        prop.SetValue(obj, Convert.ToDateTime(reader[columnName].ToString()));
                                        break;
                                    }
                                    else if (prop.PropertyType == typeof(Int32))
                                    {
                                        prop.SetValue(obj, Convert.ToInt32(reader[columnName].ToString()));
                                        break;
                                    }
                                    else if (prop.PropertyType == typeof(TimeSpan))
                                    {
                                        prop.SetValue(obj, TimeSpan.Parse(reader[columnName].ToString()));
                                        break;
                                    }
                                    else if (prop.PropertyType == typeof(Decimal))
                                    {
                                        prop.SetValue(obj, Convert.ToDecimal(reader[columnName].ToString()));
                                        break;
                                    }
                                    else
                                    {
                                        prop.SetValue(obj, reader[columnName].ToString());
                                        break;
                                    }
                                }
                            }
                        }

                        objList.Add(obj);
                    }

                }

                return objList;
            }
        }

        public async Task Insert(string cmd)
        {
            try
            {
                using (var sqlConnection =
                    new SqlConnection(ConnectionStringBuilder.BuildSQLConnectionString(_sqlSettings)))
                {
                    await sqlConnection.OpenAsync();
                    using (var sqlCmd = new SqlCommand(cmd, sqlConnection))
                    {
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                var s = 2;
            }
        }

        public async Task Update(string cmd)
        {
            using (var sqlConnection =
                new SqlConnection(ConnectionStringBuilder.BuildSQLConnectionString(_sqlSettings)))
            {
                await sqlConnection.OpenAsync();
                using (var sqlCmd = new SqlCommand(cmd, sqlConnection))
                {
                    sqlCmd.ExecuteNonQuery();
                }
            }
        }


        public async Task Delete(string cmd)
        {
            using (var sqlConnection =
                new SqlConnection(ConnectionStringBuilder.BuildSQLConnectionString(_sqlSettings)))
            {
                await sqlConnection.OpenAsync();
                using (var sqlCmd = new SqlCommand(cmd, sqlConnection))
                {
                    sqlCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
