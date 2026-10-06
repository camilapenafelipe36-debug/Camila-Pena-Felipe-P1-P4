using Microsoft.Data.Sqlite;

public static class Database
{
    private static readonly string ConnectionString = "Data Source=Data/autores.db";

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection(ConnectionString);
    }

    public static void Initialize()
    {
        using var connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Autores (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL,
                Nacionalidad TEXT NOT NULL,
                FechaNacimiento TEXT NOT NULL,
                Sueldo REAL NOT NULL
            );
            """;

        command.ExecuteNonQuery();
    }
}
