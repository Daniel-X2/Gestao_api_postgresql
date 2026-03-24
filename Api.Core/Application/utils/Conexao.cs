using Npgsql;
using DotNetEnv;

namespace Api.Core.Application.utils
{
internal interface IConnect
  {
     internal NpgsqlConnection Connect();
  }
class  ConnectHost:IConnect
  {
        private string  file = EnvDataBase();

        private static string EnvDataBase()
      {
          
          string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
          if (string.IsNullOrEmpty(connectionString))
          {
              throw new InvalidConnection();
          }
          return connectionString;
      }
     
      public NpgsqlConnection Connect() 
      {
          
          return new NpgsqlConnection (file);        
      }
  }
  

public class Load
{
    public static void LoadEnv()
    {
        Env.Load();
    }
}

}









