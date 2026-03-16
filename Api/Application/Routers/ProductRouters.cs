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
        });
        app.MapGet("/product/get", async Task<ListaProduct> (IServiceProduct service) =>
        {
            ListaProduct lista = await service.GetAllProduct();
            return lista;
            
        });
        app.MapGet("/estoque/valorBruto", async Task<List<float>> (IServiceProduct service) =>
        {
            List<float> lista = await service.GetValorBruto();

            return lista;
        });
        app.MapGet("/product/get/{id}", async Task<ProdutoDto> (int id,IServiceProduct service) =>
        {
            ProdutoDto produto = await service.GetProdutId(id);
            
            return produto;
        });
        app.MapPut("/product/update/{id}/", async Task<IResult> (int id, ProdutoDto campos, IServiceProduct service) =>
        {
            await service.UpdateProduct(campos, id);

            return Results.Ok("adicionado com Sucesso");
        });
        app.MapPost("/product/add", async Task<IResult> (ProdutoDto campos, IServiceProduct service) =>
        {
            bool resultado = await service.AddProduct(campos);
            if (resultado)
            {
                return Results.Ok("adicionado com sucesso");
            }

            return Results.BadRequest("Erro ao adicionar");
            
        });
        app.MapDelete("/product/delete/{id}", async Task<IResult> (int id, IServiceProduct service) =>
        {
            bool resultado = await service.DeleteProduct(id);
            if (resultado)
            {
                return Results.Ok("foi deletado com sucesso");
            }

            return Results.BadRequest("erro ao deletar");
            
        });
    }
}