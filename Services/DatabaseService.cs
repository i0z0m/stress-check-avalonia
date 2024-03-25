using Microsoft.Data.Sqlite;

public class DatabaseService
{
    private const string ConnectionString = "Data Source=stress_check_db.sqlite";

    public void CreateDatabase()
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