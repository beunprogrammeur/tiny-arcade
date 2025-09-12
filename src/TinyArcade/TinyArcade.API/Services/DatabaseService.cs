using Microsoft.Data.Sqlite;
using TinyArcade.API.DatabaseModels;
using TinyArcade.API.Services.Interfaces;

namespace TinyArcade.API.Services
{
    public class DatabaseService : IDatabaseService
    {
        private const string _queryNamespace = "TinyArcade.API.Queries";

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

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

        /// <summary>
        /// Run on boot
        /// </summary>
        public void Initialise()
        {
            SQLitePCL.Batteries.Init();
            ExecuteQuery("Initialise.sql");
        }

        #region security

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

        #region gaming

        public void AddConsole(DBConsole console) =>
            ExecuteQuery("AddConsole.sql",
                ("@Name", console.Name));

        public DBConsole GetConsole(int id)
        {
            using var reader = ExecuteReader("GetConsole.sql",
                ("@Id", id.ToString()));
            if (reader.Read())
            {
                return new DBConsole()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    RomFolder = reader.GetString(2)
                };
            }

            return null;
        }


        public IList<DBConsole> GetConsoles()
        {
            using var reader = ExecuteReader("GetConsoles.sql");
            List<DBConsole> consoles = [];

            while (reader.Read())
            {
                consoles.Add(new DBConsole()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }

            return consoles;
        }

        public void AddGame(DBGame game) =>
            ExecuteQuery("AddGame.sql",
                ("@ConsoleId", game.ConsoleId.ToString()),
                ("@Name", game.Name),
                ("@Description", game.Description));

        public IList<DBGame> GetGames(int consoleId)
        {
            using var reader = ExecuteReader("GetGames.sql",
                ("@ConsoleId", consoleId.ToString()));

            List<DBGame> games = [];
            while (reader.Read())
            {
                games.Add(new DBGame()
                {
                    Id = reader.GetInt32(0),
                    ConsoleId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Description = reader.GetString(3)
                });
            }

            return games;
        }

        public DBGame? GetGame(int gameId)
        {
            using var reader = ExecuteReader("GetGame.sql",
                ("@GameId", gameId.ToString()));

            if(reader.Read())
            {
                return new DBGame() 
                { 
                    ConsoleId = reader.GetInt32(0), 
                    Name = reader.GetString(1), 
                    Description = reader.GetString(2) 
                };
            }

            return null;
        }

        public IList<DBEmulator> GetEmulators()
        {
            using var reader = ExecuteReader("GetEmulators.sql");

            List<DBEmulator> emulators = [];
            while (reader.Read())
            {
                emulators.Add(new DBEmulator()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Path = reader.GetString(2),
                    Arguments = reader.GetString(3)
                });
            }

            return emulators;
        }

        public void AddEmulator(DBEmulator emulator) =>
            ExecuteQuery("AddEmulator.sql",
                ("@Name", emulator.Name),
                ("@Path", emulator.Path),
                ("@Arguments", emulator.Arguments));

        public DBEmulator? GetEmulatorByGameId(int id)
        {
            using var reader = ExecuteReader("GetEmulatorByGameId.sql",
                ("@Id", id.ToString()));

            if (reader.Read())
            {
                return new DBEmulator()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Path = reader.GetString(2),
                    Arguments = reader.GetString(3)
                };
            }

            return null;
        }


        #endregion

        #endregion
    }
}
