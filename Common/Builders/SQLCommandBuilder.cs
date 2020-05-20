using System;
using System.Text;

namespace Common.Builders
{
    public class SqlCommandBuilder
    {
        /// <summary>
        /// Gets All Records from a Table
        /// </summary>
        /// <param name="type">The Domain Object Related to the Table</param>
        /// <returns>SQL Command</returns>
        public static string GetRecords(Type type)
        {
            return $"SELECT * FROM {type.Name}";
        }

        /// <summary>
        /// Gets a Records that has an ID
        /// </summary>
        /// <param name="type">The Domain Object Related to the Table</param>
        /// <param name="id">The Id of the Records</param>
        /// <returns>SQL Command</returns>
        public static string GetIndividualRecordBuilder(Type type, Guid id)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Id = \'{id}\'";
        }

        public static string GetIndividualRecordFromNameBuilder(Type type, string name)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Name = \'{name}\'";
        }

        public static string GetIndividualRecordFromIdBuilder(Type type, Type typeToRetrieve, Guid id)
        {
            return $"SELECT * FROM {type.Name} WHERE {typeToRetrieve.Name}Id = \'{id}\'";
        }

        public static string GetIndividualRecordFromNameBuilder(Type type, Type typeToRetrieve, string name)
        {
            return $"SELECT * FROM {type.Name} WHERE {typeToRetrieve.Name}Name = {name}";
        }

        public static string GetIndividualParentFromId(Type type, Type typeToRetrieve, Guid id)
        {
            return $"SELECT * FROM {typeToRetrieve.Name} WHERE {typeToRetrieve.Name}Id = (SELECT {typeToRetrieve.Name}Id FROM {type.Name} WHERE {type.Name}Id = \'{id}\')";
        }

        public static string GetIndividualParentFromName(Type type, Type typeToRetrieve, string name)
        {
            return $"SELECT * FROM {typeToRetrieve.Name} WHERE {typeToRetrieve.Name}Id = (SELECT {typeToRetrieve.Name}Id FROM {type.Name} WHERE {type.Name}Name = \'{name}\')";
        }

        public static string GetLike(Type type, string phrase)
        {
            return $"SELECT * FROM {type.Name} WHERE {type.Name}Name LIKE \'%{phrase}%\';";
        }

        public static string InsertRecord<T>(T obj)
        {

            var str = new StringBuilder($"INSERT INTO {obj.GetType().Name} (");

            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                str.Append($"{prop.Name},");
            }
            str.Remove(str.Length - 1, 1);
            str.Append(") VALUES (");

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value.GetType() == typeof(string) || value.GetType() == typeof(Guid))
                    str.Append($"\'{value.ToString()}\',");
                else
                    str.Append($"{value.ToString()},");
            }
            str.Remove(str.Length - 1, 1);
            str.Append(");");

            return str.ToString();
        }

        public static string DeleteRecord(Type type, Guid id)
        {
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                if (prop.Name.Contains($"{type.Name}Id"))
                {
                    return $"DELETE FROM {type.Name} WHERE {prop.Name} = \'{id.ToString()}\'";
                }

            }
            return null;
        }
    }
}
