namespace Compact.Database
{
    public class DatabaseObjectManager
    {
        private SqlClient sqlClient;
        private SqlQueryFactory sqlQueryFactory;

        public DatabaseObjectManager(string connectionString)
        {
            sqlClient = new SqlClient(connectionString);
            sqlQueryFactory = new SqlQueryFactory();
        }

        public void UpdateDatabaseObject(DatabaseObject databaseObject)
        {
            var selectCountStatement = sqlQueryFactory.CreateSelectCountStatementWithId(databaseObject);
            int numRecords = sqlClient.ExecuteScalar(selectCountStatement);

            var sql = "";
            if (numRecords == 0)
                sql = sqlQueryFactory.CreateInsertStatement(databaseObject);
            else
                sql = sqlQueryFactory.CreateUpdateStatement(databaseObject);

            sqlClient.ExecuteNonQuery(sql);
        }

        public void DeleteDatabaseObject(DatabaseObject databaseObject)
        {
            var selectCountStatement = sqlQueryFactory.CreateSelectCountStatementWithId(databaseObject);
            int numRecords = sqlClient.ExecuteScalar(selectCountStatement);

            if (numRecords > 0)
            {
                var sql = sqlQueryFactory.CreateDeleteStatement(databaseObject);
                sqlClient.ExecuteNonQuery(sql);
            }
        }
    }
}
