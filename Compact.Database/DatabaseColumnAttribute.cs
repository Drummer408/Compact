using System;

namespace Compact.Database
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool PrimaryKey { get; set; }
    }
}
