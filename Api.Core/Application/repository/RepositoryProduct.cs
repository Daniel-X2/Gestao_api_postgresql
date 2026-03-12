
using Npgsql;
using Dto;
using Api.core.Application.utils;

namespace Api.core.Application.repository
{
    public interface IRepositoryProduct
{
    internal Task<ListaProduto> GetAllProduct();
    internal Task<ListaProduto> GetEstoque();
    internal  Task<List<float>> GetValorBruto();
    internal  Task<int> AddProduct(ProdutoDto campos);
    internal Task<int> UpdateProduct(ProdutoDto campos, int id);
    internal  Task<int> DeleteProduct(int id);
   internal Task<ProdutoDto> GetProductById(int id);
  internal Task<bool> IsExistLote(int lote);
  internal Task<bool> IsExistCode(int codigo);
}
class RepositoryProduct(IConnect host):IRepositoryProduct
{
    public  async Task<ProdutoDto> GetProductById(int id)
    {
        await using NpgsqlConnection connect=host.Connect(); 
        
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT * FROM produto WHERE id = @id", connect);
        cmd.Parameters.AddWithValue("id", id);
        
        await using var reader = await cmd.ExecuteReaderAsync();
        await reader.ReadAsync();
        
        ProdutoDto campos=new();
        campos.Nome=(string)reader["nome"];
        campos.Codigo=(int)reader["codigo"];
        campos.Lote=(int)reader["lote"];
        campos.Quantidade=(int)reader["quantidade"];
        campos.ValorRevenda = (float)reader["valor_revenda"];
        
         
        return campos;
    }

    public async Task<bool> IsExistLote(int lote)
    {
        await using NpgsqlConnection connect = host.Connect();
        await connect.OpenAsync();

        await using var cmd = new NpgsqlCommand("SELECT EXISTS (SELECT 1 FROM produto WHERE lote=@lote)",connect);
        cmd.Parameters.AddWithValue("lote", lote); 
        bool resultado=(bool) await cmd.ExecuteScalarAsync();
        return resultado;
    }
    public async Task<bool> IsExistCode(int codigo)
    {
       await using NpgsqlConnection connect = host.Connect();
       await connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("SELECT EXISTS (SELECT 1 FROM produto WHERE codigo=@codigo)",connect);
       cmd.Parameters.AddWithValue("codigo", codigo); 
       bool resultado=(bool) await cmd.ExecuteScalarAsync();
       return resultado;
    }
   public  async Task<ListaProduto> GetAllProduct()
    {
        
       await using NpgsqlConnection connect=host.Connect();;
       await connect.OpenAsync();
       await using var cmd = new NpgsqlCommand("SELECT * FROM produto",connect);
       await using var read= await  cmd.ExecuteReaderAsync();
       ListaProduto lista=new();
        
        while (await read.ReadAsync())
        {
            ProdutoDto campos=new();
            campos.Nome=(string)read["nome"];
            campos.Codigo=(int)read["codigo"];
            campos.Quantidade=(int)read["quantidade"];
            campos.ValorRevenda=(float)read["valor_revenda"];
            campos.Lote=(int)read["lote"];
            lista.lista_prod.Add(campos);
        }
        return lista;
     }
    public  async Task<ListaProduto> GetEstoque()
    {
       
       await using NpgsqlConnection connect=host.Connect();
       await connect.OpenAsync();

       await using var cmd=new NpgsqlCommand("SELECT nome,quantidade FROM produto",connect);
       var read=await cmd.ExecuteReaderAsync();
        ListaProduto lista=new();
        while(await read.ReadAsync())
        {
            ProdutoDto campos=new();
            campos.Nome=(string)read["nome"];
            campos.Quantidade=(int)read["quantidade"];

            lista.lista_prod.Add(campos);
        }
        return lista;
    }
    public async Task<List<float>> GetValorBruto()
    {
       
      await using  NpgsqlConnection connect=host.Connect();
      await connect.OpenAsync();

        var cmd= new NpgsqlCommand("SELECT valor_revenda FROM produto",connect);
        var read= await cmd.ExecuteReaderAsync();
        List<float> lista=new();
        while(await read.ReadAsync())
        {
            lista.Add((float)read["valor_revenda"]);
        }
        return lista;
    }
    public  async Task<int> AddProduct(ProdutoDto campos)
    {
       
      await using  NpgsqlConnection connect=host.Connect();
      await   connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("INSERT INTO produto (nome ,codigo,quantidade,valor_revenda,lote) VALUES (@nome,@codigo,@quantidade,@valor_revenda,@lote) ",connect);
        cmd.Parameters.AddWithValue("nome",campos.Nome);
        cmd.Parameters.AddWithValue("codigo", campos.Codigo);
        cmd.Parameters.AddWithValue("quantidade",campos.Quantidade);
        cmd.Parameters.AddWithValue("valor_revenda",campos.ValorRevenda);
        cmd.Parameters.AddWithValue("lote",campos.Lote);
        int resultado=await cmd.ExecuteNonQueryAsync();
        return resultado;

    }
    public async Task<int> UpdateProduct(ProdutoDto campos,int id)
    {
        
      await using  NpgsqlConnection connect= host.Connect();
      await connect.OpenAsync();

       await using var cmd = new NpgsqlCommand("UPDATE produto set nome = @nome,codigo=@codigo,quantidade=@quantidade,valor_revenda=@valor_revenda,lote=@lote WHERE id = @id",connect);
       cmd.Parameters.AddWithValue("nome",campos.Nome);
       cmd.Parameters.AddWithValue("codigo",campos.Codigo);
       cmd.Parameters.AddWithValue("lote",campos.Lote);
       cmd.Parameters.AddWithValue("quantidade",campos.Quantidade);
       cmd.Parameters.AddWithValue("valor_revenda",campos.ValorRevenda);
       cmd.Parameters.AddWithValue("id", id);
       int resultado= await cmd.ExecuteNonQueryAsync();

       return resultado;

    }
    public  async Task<int> DeleteProduct(int id)
    {
       await using NpgsqlConnection connect=host.Connect(); 
       await connect.OpenAsync();
       await using var cmd=new NpgsqlCommand("DELETE FROM produto WHERE id=@id",connect);
       cmd.Parameters.AddWithValue("id", id);
       int resultado= await cmd.ExecuteNonQueryAsync();
       return resultado;
    }

    
    
}
}
