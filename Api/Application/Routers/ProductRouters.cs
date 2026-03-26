using Dto;
using Api.Core.Application.service;


namespace Api.Routers;

public class ProductRouters
{
    public async Task Routers(WebApplication app)
    {
        app.MapGet("/estoque/get", async Task<ListaProduct> (IServiceProduct service) =>
        { 
            ListaProduct lista= await service.GetEstoque();
            
            return lista;
        }).WithTags("Product").WithSummary("Lista Quantidade e nome dos produtos no estoque");
        app.MapGet("/product/get", async Task<ListaProduct> (IServiceProduct service) =>
        {
            ListaProduct lista = await service.GetAllProduct();
            return lista;
            
        }).WithTags("Product").WithSummary("Lista todos os produtos");
        app.MapGet("/estoque/valorBruto", async Task<List<decimal>> (IServiceProduct service) =>
        {
            List<decimal> lista = await service.GetValorBruto();

            return lista;
        }).WithTags("Product").WithSummary("Lista o Valor bruto de todos os produtos");
        app.MapGet("/product/get/{id}", async Task<ProdutoDto> (int id,IServiceProduct service) =>
        {
            ProdutoDto produto = await service.GetProdutId(id);
            
            return produto;
        }).WithTags("Product").WithSummary("Lista produtos com o ID");
        app.MapPut("/product/update/{id}/", async Task<IResult> (int id, ProdutoDto campos, IServiceProduct service) =>
        {
            await service.UpdateProduct(campos, id);

            return Results.Ok("adicionado com Sucesso");
        }).WithTags("Product").WithSummary("atualiza o produto");
        app.MapPost("/product/add", async Task<IResult> (ProdutoDto campos, IServiceProduct service) =>
        {
            bool resultado = await service.AddProduct(campos);
            if (resultado)
            {
                return Results.Ok("adicionado com sucesso");
            }

            return Results.BadRequest("Erro ao adicionar");
            
        }).WithTags("Product").WithSummary("Adiciona o produto");
        app.MapDelete("/product/delete/{id}", async Task<IResult> (int id, IServiceProduct service) =>
        {
            bool resultado = await service.DeleteProduct(id);
            if (resultado)
            {
                return Results.Ok("foi deletado com sucesso");
            }

            return Results.BadRequest("erro ao deletar");
            
        }).WithTags("Product").WithSummary("Deleta o produto ");
    }
}