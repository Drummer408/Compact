using System;
using System.Collections.Generic;
using System.Reflection;

namespace Compact.Database
{
    public abstract class DatabaseObject
    {
        private string tableName;
        private List<DatabaseField> dataFields;
        private SqlQueryFactory sqlQueryFactory;

        public DatabaseObject()
        {
            SetTableName();

            dataFields = new List<DatabaseField>();
            PopulateDatabaseFieldsList(GetType().GetProperties());

            sqlQueryFactory = new SqlQueryFactory();
        }

        private void SetTableName()
        {
            var tableAttribute =
                (DatabaseTableAttribute)Attribute.GetCustomAttribute(GetType(), typeof(DatabaseTableAttribute));
            tableName = tableAttribute.Name;
        }

        private void PopulateDatabaseFieldsList(PropertyInfo[] props)
        {
            foreach (var prop in props)
            {
                var columnAttribute = (DatabaseColumnAttribute)prop.GetCustomAttribute(typeof(DatabaseColumnAttribute));
                dataFields.Add(new DatabaseField
                {
                    Name = columnAttribute.Name,
                    DataType = columnAttribute.DataType,
                    PrimaryKey = columnAttribute.PrimaryKey
                });
            }
        }
    }
}
