using System;

namespace Compact.Database
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseTableAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
