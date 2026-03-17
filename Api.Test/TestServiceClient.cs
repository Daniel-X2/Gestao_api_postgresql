
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
    [InlineData("Daniel","665.940.427-93",8585,true)]
    public async Task TestUpdateClientValido(string nome,string cpf,int conta, bool isvip)
    {
        campos.Nome = nome;
        campos.Conta = conta;
        campos.Cpf = cpf;
        campos.Isvip = isvip;
        moq.Setup(repo => repo.GetById(5)).ReturnsAsync(campos);
        moq.Setup(repo=> repo.UpdateClient(campos,5) ).ReturnsAsync(1);
        var n1 = new ClientService(moq.Object);
        try
        {
             await n1.UpdateService(5,campos);
            Assert.True(true,"o teste passou");
        }
        catch (Exception e) 
        {
            Assert.Fail($"o teste falhou por causa {e}");
        }
       
    }
    
}