using Microsoft.Data.Sqlite;

namespace stress_check_avalonia
{
    public static class DatabaseService
    {
        private const string ConnectionString = "Data Source=stress_check_db.sqlite";

        public static void CreateDatabase()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS questions (
                    id INTEGER PRIMARY KEY,
                    score INTEGER NOT NULL
                )
            ";

                command.ExecuteNonQuery();
            }
        }

        // TODO InsertOrUpdateQuestionScoreメソッドとGetQuestionScoreメソッドは後で追加
    }
}