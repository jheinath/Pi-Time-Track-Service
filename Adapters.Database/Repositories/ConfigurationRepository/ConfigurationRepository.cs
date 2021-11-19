using System;
using System.Threading.Tasks;
using Adapters.Database.Repositories.Dbos;
using Application.Configuration.Ports;
using Dapper;
using Domain.Aggregates.Configuration;
using Microsoft.Data.Sqlite;

namespace Adapters.Database.Repositories.ConfigurationRepository
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        //Todo: Move to appsettings.json and get from DI
        private const string ConnectionString = "Data Source=D:\\SQLite\\DBs\\PiTimeTrackService.db;";

        public async Task InsertAsync(Configuration configuration)
        {
            await using var conn = new SqliteConnection(ConnectionString);
            await using var cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"INSERT INTO Configuration (Id, WorkingHoursPerDay, VactionDaysCount, IsEnabled, TogglTrackAccessToken)" +
                              $"VALUES('{configuration.Id.Value}', '{configuration.WorkingHoursPerDay.Value}', '{configuration.VacationDaysCount.Value}', '{configuration.IsEnabled}', '{configuration.TogglTrackAccessToken?.Value}');";
            cmd.ExecuteNonQuery();
        }

        public async Task UpdateAsync(Configuration configuration)
        {
            throw new NotImplementedException();
            //await using var conn = new SqliteConnection(ConnectionString);
            //await using var cmd = conn.CreateCommand();
            //conn.Open();
            //cmd.CommandText = $"CREATE TABLE IF NOT EXISTS Configuration (Id IDENTiFIER PRIMARY KEY, WorkingHoursPerDay INT, VactionDaysCount INT, IsEnabled BIT, TogglTrackAccessToken nvarchar(50));";
            //cmd.ExecuteNonQuery();
        }

        public async Task<Configuration> GetAsync()
        {
            await using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var command = $"SELECT * FROM Configuration";

            var configuration = conn.QueryFirstOrDefaultAsync<ConfigurationDbo>(command);
            return Configuration.CreateNew().Value;
        }
    }
}
