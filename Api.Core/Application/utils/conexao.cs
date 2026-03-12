using Npgsql;
using DotNetEnv;

namespace Api.core.Application.utils
{
internal interface IConnect
  {
     internal NpgsqlConnection Connect();
  }
class  ConnectHost:IConnect
  {
      
      private static string EnvDataBase()
      {
          Env.Load();
          string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
          if (string.IsNullOrEmpty(connectionString))
          {
              throw new InvalidConnection();
          }
          return connectionString;
      }
     
      public NpgsqlConnection Connect() 
      {
          string  file = EnvDataBase();
          return new NpgsqlConnection (file);        
      }
    
      
  }
  
}







