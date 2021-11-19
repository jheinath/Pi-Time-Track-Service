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
                              $"VALUES('{configuration.Id.Value}', '{configuration.WorkingHoursPerDay.Value}', '{configuration.VacationDaysCount.Value}', " +
                              $"'{configuration.IsEnabled}', '{configuration.TogglTrackAccessToken?.Value}');";
            cmd.ExecuteNonQuery();
        }

        public async Task UpdateAsync(Configuration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task<Configuration> GetAsync()
        {
            await using var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            var command = $"SELECT * FROM Configuration";

            var configuration = await conn.QueryFirstOrDefaultAsync<ConfigurationDbo>(command);
            if (configuration == null)
                return null;

            return Configuration.Load(Guid.Parse(configuration.Id), configuration.WorkingHoursPerDay,
                configuration.VacationDaysCount, configuration.IsEnabled, configuration.TogglTrackAccessToken);
        }
    }
}
