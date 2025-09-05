using Microsoft.Data.Sqlite;
using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Models;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private const string _queryNamespace = "TinyArcade.API.Queries";

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IList<ConsoleModel> GetConsoles()
        {
            throw new NotImplementedException();
        }

        public IList<GameModel> GetGames(int consoleId)
        {
            throw new NotImplementedException();
        }

        public void Initialise()
        {
            SQLitePCL.Batteries.Init();

            ExecuteQuery("Initialise.sql");
        }

        #region query prep
        private string LoadQuery(string queryName)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = $"{_queryNamespace}.{queryName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            return reader.ReadToEnd();
        }

        private int ExecuteQuery(string queryFile, params (string, string)[] parameters)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = LoadQuery(queryFile);

            foreach ((var variable, var value) in parameters)
            {
                command.Parameters.AddWithValue(variable, value);
            }

            return command.ExecuteNonQuery();
        }

        private SqliteDataReader ExecuteReader(string queryFile, params (string, string)[] parameters)
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = LoadQuery(queryFile);

            foreach ((var variable, var value) in parameters)
            {
                command.Parameters.AddWithValue(variable, value);
            }

            return command.ExecuteReader();
        }
        #endregion

        #region queries

        public void AddUser(string userName, string passwordHash, string role) => 
            ExecuteQuery("AddUser.sql",
                ("@UserName", userName),
                ("@PasswordHash", passwordHash),
                ("@Role", role));

        public bool FindUser(string userName)
        {
            using var reader = ExecuteReader("FindUser.sql",
                ("@UserName", userName));

            return reader.Read() && reader.GetInt32(0) > 0;
        }
            
        public void SetUserRole(string userName, string? role) =>
            ExecuteQuery("SetUserRole.sql",
                ("@UserName", userName),
                ("@Role", role));

        public void SetUserPassword(string userName, string passwordHash) =>
            ExecuteQuery("SetUserPassword.sql",
                ("@UserName", userName),
                ("@PasswordHash", passwordHash));

        public DBUser? GetUser(string userName)
        {
            using var reader = ExecuteReader("GetUser.sql",
                ("@UserName", userName));

            if (reader.Read())
            {
                return new DBUser()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Role = reader.GetString(2),
                    PasswordHash = reader.GetString(3)
                };
            }

            return null;
        }
        #endregion
    }
}
