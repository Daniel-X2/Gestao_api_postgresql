using Npgsql;
using static System.Console;

 class  Init_repository
{
    private static string host=File.ReadAllText("/home/daniel/Pasta_boa_demais_pra_ficar_em_um_lugar/Nova pasta/projeto/Api.Core/host.txt");
   
    private protected static NpgsqlConnection Connect()
    {
        NpgsqlConnection connect=new (host);
        
        return connect;
    }
   
    
    
}







