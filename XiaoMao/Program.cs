using Npgsql;

internal class Program
{
    public class CharacterData
    {
        public string? Name { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Image { get; set; }
    }
    private static void Main(string[] args)
    {
        // Connecting to database
        const string sqlConnection =
            "Server=localhost; " +
            "Port=5432; " +
            "User Id=wenmian; " +
            "Password=4252; " +
            "Database=hogwarts";
        NpgsqlConnection currentConnection = new NpgsqlConnection();
        currentConnection.ConnectionString = sqlConnection;
        try
        {
            currentConnection.Open();
            Console.WriteLine("Connection to \"" + currentConnection.Database.ToString() + "\" succeded");
        }
        catch
        {
            Console.WriteLine("Connection failed.");
            return;
        }

        // Creating table clients
        NpgsqlCommand command = new NpgsqlCommand(
            @"CREATE TABLE IF NOT EXISTS characters(
            characterid serial PRIMARY KEY,
            firstName text,
            lastName text,
            dateofbirth date,
            image text
            )",
            currentConnection);

        NpgsqlDataReader dataReader = command.ExecuteReader();
        dataReader.Close();

        // Check if table clients exists now
        command.CommandText = @"SELECT EXISTS (
        SELECT FROM 
            pg_tables
        WHERE 
            schemaname = 'public' AND 
            tablename  = 'characters'
        );";
        dataReader = command.ExecuteReader();
        if (dataReader.Read() && dataReader.GetBoolean(0))
        {
            Console.WriteLine("Table characters exists.");
        }
        else
        {
            Console.WriteLine("Table characters doesn't exist. Stopping program.");
            return;
        }
        dataReader.Close();

        // Close databases physical connection
        currentConnection.Close();
    }
}