using Compact.Database;

namespace Compact.Sample
{
    [DatabaseTable(Name = "People")]
    class Person : DatabaseObject
    {
        [DatabaseColumn(Name = "Id", DataType = "INTEGER", PrimaryKey = true)]
        public long Id { get; set; }

        [DatabaseColumn(Name = "Name", DataType = "VARCHAR(15)")]
        public string Name { get; set; }

        [DatabaseColumn(Name = "Age", DataType = "INTEGER")]
        public int Age { get; set; }
    }
}
