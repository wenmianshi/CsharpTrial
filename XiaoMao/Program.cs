using Npgsql;

internal class Program
{
    // This Data is used for the JSON from the REST-API
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

        // Creating table characters
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

        // Check if table characters exists now (all tables will be created in pg_tables)
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

        // Requesting some characters from REST API
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://harry-potter-api-3a23c827ee69.herokuapp.com/api/characters");
        string parameters = "";
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
           );

        // Now we want to build the values so we can add them to our table characters
        // The values should look like this:
        // ('Wenmian', 'Shi', '2005-11-01', 'someImg.img'),
        // ('Pascal', 'Lefev', '2005-11-01', 'someImg.img'),
        // ('Hao', 'Yuan', '2005-11-01', 'someImg.img');
        string values = "";
        HttpResponseMessage response = client.GetAsync(parameters).Result;//wait until get the result
        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsAsync<IEnumerable<CharacterData>>().Result;
            foreach (var d in data)
            {
                if (d == null)
                {
                    continue;
                }

                if (d.Name == null || d.Name.Length == 0)
                {
                    continue;
                }

                if (d.DateOfBirth == null || d.DateOfBirth.Length == 0)
                {
                    continue;
                }

                if (d.Image == null || d.Image.Length == 0)
                {
                    continue;
                }

                if (values.Length != 0)
                {
                    values += ", ";
                }

                string firstName = "";
                string lastName = "";
                string[] name = d.Name.Split(' ');
                if (name.Count() == 1)
                {
                    firstName = name[0];
                    lastName = "Default lastname";
                }
                else
                {
                    firstName = name[0];
                    lastName = name[1];
                }
                values += "('" + firstName + "', '" + lastName + "', '" + d.DateOfBirth + "', '" + d.Image + "')";
            }

            if (values.Length != 0)
            {
                values += ";";
            }
        }
        else
        {
            Console.WriteLine("Error");
        }
        client.Dispose();

        //add some characters to my database
        if (values.Length > 0)
        {
            command.CommandText = @"INSERT INTO characters (firstname, lastname, dateofbirth, image) Values " + values;
            dataReader = command.ExecuteReader();
            dataReader.Close();
        }

        // Close databases physical connection
        currentConnection.Close();
    }
}