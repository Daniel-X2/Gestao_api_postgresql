using Npgsql;



class repository_funcionario:Init_repository
{
    
    protected internal static async Task<tipos> Get_funcionario()
    {
        await using NpgsqlConnection connect=Connect();
        
        await connect.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM funcionario", connect);
       
        tipos lista=new();

        await using var reader = await cmd.ExecuteReaderAsync();
        while(await reader.ReadAsync())
        {
            funcionario campos=new();
            campos.nome=(string)reader["nome"];
            campos.cpf=(string)reader["cpf"];
            campos.isadmin=(bool)reader["isadmin"];
            campos.quantidade_atestado=(int)reader["quantidade_atestado"];
            campos.nascimento=(int)reader["nascimento"];
            lista.lista_funcionario.Add(campos);
        }
         
        return lista;
    }
    protected internal static async Task<int> add_funcionario(string nome,int cpf,int quantidade_atestado,bool isadmin,int nascimento)
    {
        int resultado;
        await using NpgsqlConnection connect = Connect();
        
        await connect.OpenAsync();

        await using (var cmd = new NpgsqlCommand("INSERT INTO funcionario (nome ,cpf, isadmin,quantidade_atestado,nascimento) VALUES (@nome ,@cpf, @isadmin,@quantidade_atestado,@nascimento)", connect))
        {
            cmd.Parameters.AddWithValue("nome", nome);
            cmd.Parameters.AddWithValue("cpf", cpf);
            cmd.Parameters.AddWithValue("isadmin", isadmin);
            cmd.Parameters.AddWithValue("quantidade_atestado", quantidade_atestado);
            cmd.Parameters.AddWithValue("nascimento",nascimento);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado;
    }  
    
    public static async Task<int> atualizar_funcionario(string antigo_nome,string novo_nome)
    {
        
        await using NpgsqlConnection connect=Connect();

        await connect.OpenAsync();
        int resultado;
        using (var cmd=new NpgsqlCommand("UPDATE  funcionario set nome = @nome WHERE nome =  @antigo_nome", connect))
        {
            cmd.Parameters.AddWithValue("nome",novo_nome);
            cmd.Parameters.AddWithValue("antigo_nome",antigo_nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
         return  resultado;

    }
     public static async Task<int> delete_funcionario(string nome)
    {
        int resultado;
        await using NpgsqlConnection connect=Connect();

        await connect.OpenAsync();
        //revisar e colocar pra pegar por id
        using (var  cmd = new NpgsqlCommand("DELETE FROM funcionario WHERE nome = @nome ", connect))
        {
            cmd.Parameters.AddWithValue("nome",nome);
            resultado=await cmd.ExecuteNonQueryAsync();
        }
        
        return resultado ;
        
        
    }


}