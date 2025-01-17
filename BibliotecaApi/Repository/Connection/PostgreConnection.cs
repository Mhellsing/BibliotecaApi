using Npgsql;
using System.Data;

namespace BibliotecaApi.Repository.Connection
{
    public class PostgreConnection : IDisposable
    {
        public IDbConnection Connection { get;}
        
        public PostgreConnection(IConfiguration configuration) 
        {
            Connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));            
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
