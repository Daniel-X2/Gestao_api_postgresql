using static System.Console;
using Npgsql;

class deposito:Init_repository
{
    public static async Task<tipos> get_produto()
    {
       await using NpgsqlConnection connect= Connect();
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM produto",connect);
       await using var read= await  cmd.ExecuteReaderAsync();
        tipos lista=new();
        
        while (await read.ReadAsync())
        {
            produto campos=new();
            campos.nome=(string)read["nome"];
            campos.codigo=(int)read["codigo"];
            campos.quantidade=(int)read["quantidade"];
            campos.valor_revenda=(float)read["valor_revenda"];
            campos.lote=(int)read["lote"];
            lista.lista_produto.Add(campos);
        }
      return lista;
     }
    public static async Task<tipos> get_estoque()
    {
       await using NpgsqlConnection connect=Connect();
        await connect.OpenAsync();

        var cmd=new NpgsqlCommand("SELECT nome,quantidade FROM produto",connect);
        var read=await cmd.ExecuteReaderAsync();
        tipos lista=new();
        while(await read.ReadAsync())
        {
            produto campos=new();
            campos.nome=(string)read["nome"];
            campos.quantidade=(int)read["quantidade"];

            lista.lista_produto.Add(campos);
        }
        return lista;
    }
    public static async Task<List<double>> get_valor_bruto()
    {
      await using  NpgsqlConnection connect=Connect();
        await connect.OpenAsync();

        var cmd= new NpgsqlCommand("SELECT valor_revenda FROM produto",connect);
        var read= await cmd.ExecuteReaderAsync();
        List<double> lista=new();
        while(await read.ReadAsync())
        {
            
            lista.Add((float)read["valor_revenda"]);
        }
        return lista;
    }
    public static async Task<int> add_produto(string nome,int codigo,int quantidade,double valor_revenda,int lote)
    {
      await using  NpgsqlConnection connect=Connect();
      await   connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("INSERT INTO produto (nome ,codigo,quantidade,valor_revenda,lote) VALUES (@nome,@codigo,@quantidade,@valor_revenda,@lote) ",connect);
        cmd.Parameters.AddWithValue("nome",nome);
        cmd.Parameters.AddWithValue("codigo",codigo);
        cmd.Parameters.AddWithValue("quantidade",quantidade);
        cmd.Parameters.AddWithValue("valor_revenda",valor_revenda);
        cmd.Parameters.AddWithValue("lote",lote);
        int resultado=await cmd.ExecuteNonQueryAsync();
        return resultado;

    }
    public static async Task<int> atualizar_produto(string nome_antigo,string novo_nome)
    {
      await using  NpgsqlConnection connect= Connect();
        await connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("UPDATE produto set nome = @novo_nome WHERE nome = @nome_antigo",connect);
       cmd.Parameters.AddWithValue("nome_antigo",nome_antigo);
       cmd.Parameters.AddWithValue("novo_nome",novo_nome);
       int resultado= await cmd.ExecuteNonQueryAsync();

       return resultado;

    }
    public static async Task<int> delete_produto(string nome)
    {
       await using NpgsqlConnection connect=Connect();
        await connect.OpenAsync();

       await using var cmd=new NpgsqlCommand("DELETE FROM produto WHERE nome = @nome",connect);
        cmd.Parameters.AddWithValue("nome",nome);

       int resultado= await cmd.ExecuteNonQueryAsync();

       return resultado;

    }
}