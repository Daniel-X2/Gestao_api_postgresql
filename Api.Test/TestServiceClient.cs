
using Api.Core.Application.repository;
using Api.Core.Application.service;
using Dto;

using Moq;
using Xunit;

namespace Api.Test;

public class TestServiceClient
{
    Mock<IRepositoryClient> moq = new();
    private ClientDto campos = new();
    [Theory]
    [InlineData("Daniel","665.940.427-93",875,true)]
    public async Task TestAddClientValido(string nome,string cpf,int conta, bool isvip)
    {
        
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        moq.Setup(repo=> repo.AddClient(campos) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        
        await n1.AddService(campos);
        
    }
    [Theory]
    [InlineData("Daniel","57852115",55,true)]
    public async Task TestAddClientInvalidoCpf(string nome,string cpf,int conta, bool isvip)
    {
        
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        moq.Setup(repo=> repo.AddClient(campos) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        
        
        await Assert.ThrowsAsync<InvalidCpfException>(async ( )=> await n1.AddService(campos));
        
    }
    [Theory]
    [InlineData("Daniel","665.940.427-93",-15,true)]
    public async Task TestAddClientInvalidoConta(string nome,string cpf,int conta, bool isvip)
    {
        
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        moq.Setup(repo=> repo.AddClient(campos) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        
        
        await Assert.ThrowsAsync<InvalidAccountException>(async ( )=> await n1.AddService(campos));
        
    }
    [Theory]
    [InlineData("Da","665.940.427-93",484,false)]
    public async Task TestAddClientInvalidoNome(string nome,string cpf,int conta, bool isvip)
    {
        
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        moq.Setup(repo=> repo.AddClient(campos) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        
        await Assert.ThrowsAsync<InvalidNameException>(async ( )=> await n1.AddService(campos));
        
    }
    [Theory]
    [InlineData("Daniel","665.940.427-93",8585,true)]
    public async Task TestUpdateClientValido(string nome,string cpf,int conta, bool isvip)
    {
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        //eu pego outro que ele pega pelo id e faço o retorno ser igual zero
        moq.Setup(repo => repo.GetById(5)).ReturnsAsync(campos);
        moq.Setup(repo=> repo.UpdateClient(campos,5) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        
        await n1.UpdateService(5,campos);
        Assert.True(true,"o teste passou");
    }
    [Theory]
    [InlineData(-1)]
    public async Task TestDeleteClientInValido(int id)
    {
        moq.Setup(repo => repo.DeleteClient(id)).ReturnsAsync(0);
        var n1 = new ClientService(moq.Object);
        
        await Assert.ThrowsAsync<InvalidIdException>(async () => await n1.DeleteService(id));

    }

    [Theory]
    [InlineData(5)]
    public async Task TestDeleteClientValido(int id)
    {
        moq.Setup(repo => repo.DeleteClient(id)).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        bool resultado =await n1.DeleteService(id);
        Assert.True(resultado);
    }
    
}