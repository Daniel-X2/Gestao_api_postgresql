using Npgsql;
using Dto;

namespace Api.core.Application.repository
{
public interface  IRepositoryClient
{
   internal Task<ListaClient>  GetAllClient();
   internal Task<ClientDto> GetById(int id);
   internal Task<int> AddClient(string nome,string cpf,int conta,bool isvip);
   internal Task<bool> ContaExiste(int conta);
   internal Task<bool> IsExistsCpf(string cpf);
   internal Task<int> UpdateClient(ClientDto campos, int id);
   internal Task<int> DeleteClient(int id);
   internal Task<int> GetIdByCpf(string cpf);

}
internal class RepositoryClient(IConnect host):IRepositoryClient
{
    
    public async Task<ListaClient>  GetAllClient()
    {

        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente ", connect);

        ListaClient lista=new();
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            ClientDto campos=new();
            campos.Nome=(string)reader["nome"];
            campos.Cpf=(string)reader["cpf"];
            campos.Conta=(int)reader["conta"];
            campos.Isvip=(bool)reader["isvip"];
            lista.lista_client.Add(campos);
        }
         
        return lista;
    }
    public  async Task<ClientDto> GetById(int id)
    {
        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente WHERE id = @id", connect);
        cmd.Parameters.AddWithValue("id", id);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        
        ClientDto campos=new();
        campos.Nome=(string)reader["nome"];
        campos.Cpf=(string)reader["cpf"];
        campos.Conta=(int)reader["conta"];
        campos.Isvip=(bool)reader["isvip"];
           
        
         
        return campos;
    }
    public  async Task<int> AddClient(string nome,string cpf,int conta,bool isvip)
    {
        int resultado;
        
        await using NpgsqlConnection connect = host.Connect();
        
        await connect.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO cliente (nome ,cpf, conta,isvip) VALUES (@nome, @cpf, @conta,@isvip)", connect))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("conta", conta);
            cmd.Parameters.AddWithValue("isvip", isvip);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado;
    }  
    public async Task<bool> ContaExiste(int conta)
    {
        await using NpgsqlConnection connect=host.Connect();
        await connect.OpenAsync();
        string sql="SELECT EXISTS (SELECT 1 FROM cliente WHERE conta=@conta)";
        await using var cmd=new NpgsqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("conta",conta);
       bool resultado=(bool) await cmd.ExecuteScalarAsync();
       return resultado;
       
    }
    public async Task<bool> IsExistsCpf(string cpf)
    {
        await using NpgsqlConnection connect =host.Connect();
        await connect.OpenAsync();
        string sql ="SELECT EXISTS (SELECT 1 FROM cliente WHERE cpf=@cpf)";
        await using var cmd=new NpgsqlCommand(sql,connect);
        cmd.Parameters.AddWithValue("cpf",cpf);
        bool resultado=(bool) await cmd.ExecuteScalarAsync();
        return resultado;
    }
    public  async Task<int> UpdateClient(ClientDto campos,int id)
    {
        
        await using NpgsqlConnection connect=host.Connect();

        await connect.OpenAsync();
        int resultado;
        string sql = "UPDATE  cliente set nome=@nome,cpf=@cpf,conta=@conta,isvip=@isvip  WHERE id = @id";
        
       await using var cmd = new NpgsqlCommand("UPDATE  cliente set nome=@nome,cpf=@cpf,conta=@conta,isvip=@isvip  WHERE id = @id", connect);
      
       cmd.Parameters.AddWithValue("conta",campos.Conta);
       cmd.Parameters.AddWithValue("isvip",campos.Isvip);
       cmd.Parameters.AddWithValue("nome", campos.Nome);
       cmd.Parameters.AddWithValue("cpf", campos.Cpf);
       cmd.Parameters.AddWithValue("id", id);
       
        
         resultado=await cmd.ExecuteNonQueryAsync();   
        
         return  resultado;
    }
    public  async Task<int> DeleteClient(int id)
    {
        int resultado;
        
        await using NpgsqlConnection connect=host.Connect();
        
        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
       await using (var  cmd = new NpgsqlCommand("DELETE FROM cliente WHERE id=@id ", connect))
        {
            cmd.Parameters.AddWithValue("id",id);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
       
        return resultado ;
        
        
    }
    public  async Task<int> GetIdByCpf(string cpf)
    {
        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT id FROM cliente WHERE cpf=@cpf ", connect);
        cmd.Parameters.AddWithValue("cpf", cpf);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();




        
        return (int) reader["id"];
    }
    
}
}