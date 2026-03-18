using Dto;
using Xunit;
using Api.Core.Application.repository;
using Api.Core.Application.service;
using Moq;
namespace Api.Test;

public class TestServiceProduct
{
    Mock<IRepositoryProduct> moq = new();
    private ProdutoDto campos = new();
    [Theory]
    [InlineData("I5 5560",1575,10,55,250.00f)]
    public async Task TestAddProduct(string nome,int codigo,int quantidade,int lote,float valor_revenda)
    {
        campos.Nome = nome;
        campos.Codigo = codigo;
        campos.Lote = lote;
        campos.Quantidade = quantidade;
        campos.ValorRevenda = valor_revenda;

        moq.Setup(repo => repo.AddProduct(campos)).ReturnsAsync(1);
        
        var n1 =new  ServiceProduct(moq.Object);
        await  n1.AddProduct(campos);
    }
    [Theory]
    [InlineData("I5 5560",-1,10,55,250.00f)]
    public async Task TestAddInvalidCode(string nome,int codigo,int quantidade,int lote,float valor_revenda)
    {
        campos.Nome = nome;
        campos.Codigo = codigo;
        campos.Lote = lote;
        campos.Quantidade = quantidade;
        campos.ValorRevenda = valor_revenda;
        moq.Setup(repo => repo.AddProduct(campos)).ReturnsAsync(1);
        var n1 =new  ServiceProduct(moq.Object);

        await Assert.ThrowsAsync<InvalidCodeException>(async () => await n1.AddProduct(campos));
    }
    [Theory]
    [InlineData("I5 5560",15795,10,-1,250.00f)]
    public async Task TestAddInvalidlote(string nome,int codigo,int quantidade,int lote,float valor_revenda)
    {
        campos.Nome = nome;
        campos.Codigo = codigo;
        campos.Lote = lote;
        campos.Quantidade = quantidade;
        campos.ValorRevenda = valor_revenda;
        moq.Setup(repo => repo.AddProduct(campos)).ReturnsAsync(1);
        var n1 =new  ServiceProduct(moq.Object);

        await Assert.ThrowsAsync<InvalidLoteException>(async () => await n1.AddProduct(campos));
    }
    [Theory]
    [InlineData("I5 5560",15795,10,-1,5)]
    public async Task TestAddInvalidValor(string nome,int codigo,int quantidade,int lote,float valor_revenda)
    {
        campos.Nome = nome;
        campos.Codigo = codigo;
        campos.Lote = lote;
        campos.Quantidade = quantidade;
        campos.ValorRevenda = valor_revenda;
        moq.Setup(repo => repo.AddProduct(campos)).ReturnsAsync(1);
        var n1 =new  ServiceProduct(moq.Object);

        await Assert.ThrowsAsync<InvalidL>(async () => await n1.AddProduct(campos));
    }
}