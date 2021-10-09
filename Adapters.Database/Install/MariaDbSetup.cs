using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Adapters.Database.Install
{
    public class MariaDbSetup
    {
        private const string DatabaseName = "PiTimeTrack";
        public async Task Install()
        {
            string connStr = "server=localhost;user=root;port=3306;password=mysql;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{DatabaseName}`;";
                cmd.ExecuteNonQuery();
            }
        }

        public string BuildSetupCommand()
        {
            return new StringBuilder().AppendLine("").ToString();
        }
        

    }
}
