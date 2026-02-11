using Xunit;
using static repository_client;

public class Test_client
{
    [Fact]
    public async Task test_add_client()
    {
        string nome="Daniel";
        int cpf=457665;
        int conta=5555;
        bool isvip=true;
        int resultado= await add_client(nome,cpf,conta,isvip);
        
        await delete(nome);
        Assert.NotEqual(0,resultado);
      
    }
    [Fact]
    public async Task test_atualizar_client()
    {
        string antigo_nome="felipe";
        await add_client(antigo_nome,4,4,false);
        
        string novo_nome="cleitonn";
        int resultado= await atualizar_client(antigo_nome,novo_nome);

        await delete(novo_nome);
        Assert.NotEqual(0,resultado);
    
        
    }
    [Fact]
    public async Task test_delete_client()
    {
        string nome="elton";
        await add_client(nome,4,4,false);
        
        int resultado = await delete(nome);
        Assert.NotEqual(0,resultado);
    }

    [Fact]
    public async Task test_get_client()
    {
        string nome="clei";
        await add_client(nome,4,4,false);

       var resultado= await Get_client();
       
       Assert.NotEqual(0,resultado.lista_client[0].Nome.Length);
       
       await delete(nome);
       
    }
}

