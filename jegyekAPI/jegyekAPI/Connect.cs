using MySql.Data.MySqlClient;

namespace jegyekAPI
{
    public class Connect
    {
        public MySqlConnection connection;
        private string Host;
        private string DbName;
        private string UserName;
        private string Password;
        private string ConnectionString;

        public Connect()
        {
            Host = "localhost";
            DbName = "jegyek";
            UserName = "root";
            Password = "";

            ConnectionString = $"Host={Host};Database={DbName};User={UserName};Password={Password};SslMode=None";

            connection = new MySqlConnection(ConnectionString);

        }
    }
}
