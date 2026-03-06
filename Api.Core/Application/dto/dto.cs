
using System.Runtime.CompilerServices;

namespace Dto
{
    public class ListaClient
    {
        public List<ClientDto> lista_client {get;set;}= new List<ClientDto>();
    }
    
public class ListaFuncionario
{
    public List<FuncionarioDto> lista_funci {get;set;}= new List<FuncionarioDto>();
}
public class ListaProduto
{
     public List<ProdutoDto> lista_prod {get;set;}=new List<ProdutoDto>();
}
public class ClientDto
{
    public string Nome{get;set;}
    public string Cpf{get;set;}
    public int Conta{get;set;}
    public bool Isvip{get;set;}
    
}
public class FuncionarioDto
{
    
    public string Nome{get;set;}
    public string Cpf{get;set;}
    public bool Isadmin{get;set;}
    public int QuantidadeAtestado{get;set;}
    public int Nascimento{get;set;}
}
public class ProdutoDto
{
    public string Nome{get;set;}
    public int Codigo{get;set;}
    public int Quantidade{get;set;}
    public float ValorRevenda{get;set;}
    public int Lote{get;set;}
}
}
