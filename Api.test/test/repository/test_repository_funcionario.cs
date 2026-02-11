using Xunit;
using static repository_funcionario;
public class test_funcionario
{
    [Fact]
    public async Task test_add_funcionario()
    {
        string nome="Joao";
        int cpf=457665;
        int quantidade_atestado=5;
        int nascimento=2007;
        bool isadmim=true;
        int resultado= await add_funcionario(nome,cpf,quantidade_atestado,isadmim,nascimento);
        Assert.NotEqual(0,resultado);
        await delete_funcionario(nome);
    }
    [Fact]
    public async Task test_atualizar_client()
    {
        string antigo_nome="Daniel";
        await add_funcionario(antigo_nome,44,4,true,2005);
        
        string novo_nome="elton";
        int resultado= await atualizar_funcionario(antigo_nome,novo_nome);
        Assert.NotEqual(0,resultado);

        await delete_funcionario(novo_nome);
        
    }
    [Fact]
    public async Task test_delete_client()
    {
        string nome="felipe";
        await add_funcionario(nome,4,4,false,2005);
        
        int resultado = await delete_funcionario(nome);
        Assert.NotEqual(0,resultado);
    }

    [Fact]
    public async Task test_get_client()
    {
        string nome="cleiton";
        await add_funcionario(nome,4,4,false,2005);

       var resultado= await Get_funcionario();
       
       Assert.NotEqual(0,resultado.lista_funcionario[0].nome.Length);
       
       await delete_funcionario(nome);
       
    }
}