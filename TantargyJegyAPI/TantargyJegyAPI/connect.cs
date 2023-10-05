using MySql.Data.MySqlClient;

namespace TantargyJegyAPI
{
    public class connect
    {
        public MySqlConnection connection;
        private string Host;
        private string DbName;
        private string UserName;
        private string Password;
        private string ConnectionString;

        public connect()
        {
            Host = "localhost";
            DbName = "jegyek";
            UserName = "root";
            Password = "";

  

            ConnectionString = $"Host={Host};Database={DbName};User={UserName};Password={Password};Ssl Mode=None";

            connection = new MySqlConnection(ConnectionString);

        }
    }
}
