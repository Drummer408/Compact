using System;
using System.Collections.Generic;
using System.Reflection;

namespace Compact.Database
{
    public abstract class DatabaseObject
    {
        private string tableName;
        private string primaryKey;
        private IDictionary<string, DatabaseValue> databaseFields;

        protected DatabaseObject()
        {
            tableName = GetTableNameFromType(GetType());
        }

        private string GetTableNameFromType(Type type)
        {
            var tableAttribute =
                (DatabaseTableAttribute) Attribute.GetCustomAttribute(type, typeof(DatabaseTableAttribute));
            return tableAttribute.Name;
        }

        public void SetProperties()
        {
            databaseFields = new Dictionary<string, DatabaseValue>();

            var props = GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(this);

                var columnAttribute =
                    (DatabaseColumnAttribute) prop.GetCustomAttribute(typeof(DatabaseColumnAttribute));
                AddDatabaseField(value, columnAttribute);

                // Do primary key check
                if (columnAttribute.PrimaryKey)
                {
                    if (string.IsNullOrEmpty(primaryKey))
                    {
                        primaryKey = columnAttribute.Name;
                        continue;
                    }

                    throw new Exception("Each database object can have only one primary key.");
                }
            }
        }

        internal string GetTableName()
        {
            return tableName;
        }

        internal string GetPrimaryKey()
        {
            return primaryKey;
        }

        internal IDictionary<string, DatabaseValue> GetDatabaseFields()
        {
            return databaseFields;
        }

        private void AddDatabaseField(object value, DatabaseColumnAttribute columnAttribute)
        {
            var databaseValue = CreateDatabaseValue(value.ToString(), columnAttribute.DataType,
                columnAttribute.PrimaryKey);
            databaseFields[columnAttribute.Name] = databaseValue;
        }

        private DatabaseValue CreateDatabaseValue(string value, string dataType, bool isPrimaryKey)
        {
            return new DatabaseValue(dataType, isPrimaryKey, value);
        }
    }
}
