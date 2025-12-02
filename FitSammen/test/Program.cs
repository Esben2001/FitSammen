using FitSammen_API.Security;
using Microsoft.Data.SqlClient;
string? useConnectionString = "Server=ESBEN\\SQLEXPRESS;Database=FitSammenDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

using var conn = new SqlConnection(useConnectionString);
await conn.OpenAsync();

// 1. Hent alle brugere med klartekst-password
var selectCmd = conn.CreateCommand();
selectCmd.CommandText = "SELECT [User_ID], [Password] FROM [User]";
using var reader = await selectCmd.ExecuteReaderAsync();

var users = new List<(int Id, string Password)>();
while (await reader.ReadAsync())
{
    int id = reader.GetInt32(0);
    string pwd = reader.GetString(1);
    users.Add((id, pwd));
}

reader.Close();

// 2. For hver bruger, lav hash + salt og opdater
foreach (var u in users)
{
    PasswordHasher.CreatePasswordHash(u.Password, out byte[] hash, out byte[] salt);

    using var updateCmd = conn.CreateCommand();
    updateCmd.CommandText = @"
        UPDATE [User]
        SET PasswordHash = @hash,
            PasswordSalt = @salt
        WHERE User_ID = @id";

    updateCmd.Parameters.AddWithValue("@hash", hash);
    updateCmd.Parameters.AddWithValue("@salt", salt);
    updateCmd.Parameters.AddWithValue("@id", u.Id);

    await updateCmd.ExecuteNonQueryAsync();
}