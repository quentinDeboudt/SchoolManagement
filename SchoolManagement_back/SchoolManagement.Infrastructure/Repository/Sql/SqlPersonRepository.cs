public class SqlPersonRepository : IPersonRepository
{
    private readonly string _connectionString;

    public SqlPersonRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT Id, FirstName, LastName FROM Persons WHERE Id = @Id";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Person
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2)
            };
        }

        return null;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        var persons = new List<Person>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT Id, FirstName, LastName FROM Persons";
        var command = new SqlCommand(query, connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            persons.Add(new Person
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2)
            });
        }

        return persons;
    }

    public async Task AddAsync(Person person)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "INSERT INTO Persons (FirstName, LastName) VALUES (@FirstName, @LastName)";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@FirstName", person.FirstName);
        command.Parameters.AddWithValue("@LastName", person.LastName);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "UPDATE Persons SET FirstName = @FirstName, LastName = @LastName WHERE Id = @Id";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@FirstName", person.FirstName);
        command.Parameters.AddWithValue("@LastName", person.LastName);
        command.Parameters.AddWithValue("@Id", person.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "DELETE FROM Persons WHERE Id = @Id";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        await command.ExecuteNonQueryAsync();
    }
}
