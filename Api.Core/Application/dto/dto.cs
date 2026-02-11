

class tipos//esse aqui e o dto do repository client subsituir o task<list<>>
{
    
    public List<client> lista_client {get;set;}= new List<client>();
    public List<funcionario> lista_funcionario {get;set;}= new List<funcionario>();
    public List<produto> lista_produto {get;set;}=new List<produto>();
}
class client
{
    public string Nome{get;set;}
    public string cpf{get;set;}
    public int conta{get;set;}
    public bool isvip{get;set;}
    
}
class funcionario
{
    public string nome{get;set;}
    public string cpf{get;set;}
    public bool isadmin{get;set;}
    public int quantidade_atestado{get;set;}
    public int nascimento{get;set;}
}
class produto
{
    public string nome{get;set;}
    public int codigo{get;set;}
    public int quantidade{get;set;}
    public double valor_revenda{get;set;}
    public int lote{get;set;}
}