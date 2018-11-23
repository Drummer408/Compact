using System;
using System.Collections.Generic;

namespace Compact.Database
{
    internal class DatabaseValue
    {
        private static IList<string> SqlNumericTypes = new List<string>(new [] { "INTEGER", "DECIMAL" });

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!IsPrimaryKey)
                    _value = value;
                else
                    throw new Exception("Cannot modify the primary key.");
            }
        }

        public GenericSqlDataType GenericSqlDataType { get; private set; }
        public bool IsPrimaryKey { get; }

        public DatabaseValue(string dataType, bool isPrimaryKey, string value)
        {
            Value = value;
            IsPrimaryKey = isPrimaryKey;
            SetSqlDataType(dataType);
        }

        private void SetSqlDataType(string inputType)
        {
            foreach (string sqlType in SqlNumericTypes)
            {
                if (inputType.ToUpper().StartsWith(sqlType))
                {
	                GenericSqlDataType = GenericSqlDataType.Numeric;
                    return;
                }
            }

	        GenericSqlDataType = GenericSqlDataType.String;
        }
    }
}

