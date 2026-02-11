using Npgsql;


class repository_client: Init_repository
{
    
    
    protected internal static async Task<tipos> Get_client()
    {
        await using NpgsqlConnection connect=Connect();
        
        await connect.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM cliente", connect);
       
        tipos lista=new();
        
        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            client campos=new();
            campos.Nome=(string)reader["nome"];
            campos.cpf=(string)reader["cpf"];
            campos.conta=(int)reader["conta"];
            campos.isvip=(bool)reader["isvip"];
            lista.lista_client.Add(campos);
        }
         
        return lista;
    }
    protected internal static async Task<int> add_client(string nome,int cpf,int conta,bool isvip)
    {
        int resultado;
        await using NpgsqlConnection connect = Connect();
        
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
    
    public static async Task<int> atualizar_client(string antigo_nome,string novo_nome)
    {
        
        await using NpgsqlConnection connect=Connect();

        await connect.OpenAsync();
        int resultado;
        using (var cmd=new NpgsqlCommand("UPDATE  cliente set nome = @nome WHERE nome =  @antigo_nome", connect))
        {
            cmd.Parameters.AddWithValue("nome",novo_nome);
            cmd.Parameters.AddWithValue("antigo_nome",antigo_nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
         return  resultado;

    }
    public static async Task<int> delete(string nome)
    {
        int resultado;
        await using NpgsqlConnection connect=Connect();

        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
        using (var  cmd = new NpgsqlCommand("DELETE FROM cliente WHERE nome = @nome ", connect))
        {
            cmd.Parameters.AddWithValue("nome",nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado ;
        
        
    }
    
}



