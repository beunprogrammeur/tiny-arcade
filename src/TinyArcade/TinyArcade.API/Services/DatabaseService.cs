using Microsoft.Data.Sqlite;
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

            string query = LoadQuery("Initialise.sql");
            ExecuteQuery(query);
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

        private void ExecuteQuery(string query, params Tuple<string, string>[] parameters)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            
            var command = connection.CreateCommand();
            command.CommandText = query;

            foreach ((var variable, var value) in parameters)
            {
                command.Parameters.AddWithValue(variable, value);
            }

            command.ExecuteNonQuery();
        }
        #endregion

        #region queries

        public void AddUser(string username, string passwordHash, string role)
        {
            string query = LoadQuery("AddUser.sql");
            ExecuteQuery(query,
                Tuple.Create("@Username", username),
                Tuple.Create("@PasswordHash", passwordHash),
                Tuple.Create("@Role", role));
        }
        #endregion
    }
}
