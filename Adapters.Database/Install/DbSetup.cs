using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Adapters.Database.Install
{
    public class DbSetup : IDbSetup
    {
        private const string ConnectionString = "Data Source=D:\\SQLite\\DBs\\PiTimeTrackService.db;";

        public async Task Install()
        {
            await using var conn = new SqliteConnection(ConnectionString);
            await using var cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS Configuration (Id TEXT PRIMARY KEY, WorkingHoursPerDay INT, VactionDaysCount INT, IsEnabled BIT, TogglTrackAccessToken nvarchar(50));";
            cmd.ExecuteNonQuery();
        }
    }

    public interface IDbSetup
    {
        Task Install();
    }
}
