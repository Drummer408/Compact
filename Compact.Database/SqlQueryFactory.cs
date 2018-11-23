namespace Compact.Database
{
    public class SqlQueryFactory
    {
        public string CreateInsertStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var parameterListBuilder = SqlQueryFactoryUtility.CreateInsertParameterNameValueBuilder(databaseObject.GetDatabaseFields());

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                tableName,
                parameterListBuilder.NameBuilder.ToString(),
                parameterListBuilder.ValueBuilder.ToString());
        }

        public string CreateUpdateStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var primaryKey = databaseObject.GetPrimaryKey();

            return string.Format("UPDATE {0} SET {1} WHERE {2}",
                tableName,
                SqlQueryFactoryUtility.CreateUpdateList(databaseObject.GetDatabaseFields()),
                SqlQueryFactoryUtility.CreateEqualityClause(primaryKey, databaseObject.GetDatabaseFields()[primaryKey]));
        }

        public string CreateDeleteStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var primaryKey = databaseObject.GetPrimaryKey();

            return string.Format("DELETE FROM {0} WHERE {1}",
                tableName,
                SqlQueryFactoryUtility.CreateEqualityClause(primaryKey, databaseObject.GetDatabaseFields()[primaryKey]));
        }

        public string CreateSelectCountStatementWithId(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var primaryKey = databaseObject.GetPrimaryKey();

            return string.Format("SELECT COUNT(*) FROM {0} WHERE {1}",
                tableName,
                SqlQueryFactoryUtility.CreateEqualityClause(primaryKey, databaseObject.GetDatabaseFields()[primaryKey]));
        }
    }
}
