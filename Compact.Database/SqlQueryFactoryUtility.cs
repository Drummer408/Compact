using System.Collections.Generic;
using System.Text;

namespace Compact.Database
{
    internal static class SqlQueryFactoryUtility
    {
        internal static InsertParameterNameValueListBuilder CreateInsertParameterNameValueBuilder(IDictionary<string, DatabaseValue> databaseFields)
        {
            var paramBuilder = new InsertParameterNameValueListBuilder();

            int count = 0;
            foreach (var key in databaseFields.Keys)
            {
                paramBuilder.NameBuilder.Append(key);
                paramBuilder.ValueBuilder.Append(GetDatabaseValue(databaseFields[key]));

                if (count < databaseFields.Count - 1)
                {
                    paramBuilder.NameBuilder.Append(", ");
                    paramBuilder.ValueBuilder.Append(", ");
                }

                count++;
            }

            return paramBuilder;
        }

        internal static string CreateUpdateList(IDictionary<string, DatabaseValue> databaseFields)
        {
            var updateSetBuilder = new StringBuilder();

            int count = 0;
            foreach (var key in databaseFields.Keys)
            {
                updateSetBuilder.Append(CreateEqualityClause(key, databaseFields[key]));

                if (count < databaseFields.Count - 1)
                {
                    updateSetBuilder.Append(", ");
                }

                count++;
            }

            return updateSetBuilder.ToString();
        }

        internal static string CreateEqualityClause(string key, DatabaseValue value)
        {
            var databaseValue = GetDatabaseValue(value);
            return key + " = " + databaseValue;
        }

        private static string GetDatabaseValue(DatabaseValue value)
        {
            var stringValue = value.Value;
            if (value.GenericSqlDataType == GenericSqlDataType.String)
                stringValue = $"'{stringValue}'";

            return stringValue;
        }
    }
}
