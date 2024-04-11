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

        // Close databases physical connection
        currentConnection.Close();
    }
}