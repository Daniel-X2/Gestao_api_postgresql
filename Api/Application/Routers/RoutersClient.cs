using Api.core.Application.service;
using Dto;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sprache;

namespace api.Routers;

public class RoutersClient
{
    public static async Task Routers(WebApplication app)
    {
        app.MapGet("/client/get", async Task<ListaClient> (IServiceCLient service) =>
        {
         ListaClient lista=  await service.GetAllService();
         return lista;
        });
        app.MapDelete("client/delete/{id}", async Task<IResult> (int id,IServiceCLient service) =>
        {
          bool resultado= await service.DeleteService(id);
          if (resultado)
          {
              return Results.Ok("deletado com sucesso");
          }
          return Results.BadRequest("nao foi possivel deletar");
        });
        app.MapPost("client/add/{nome},{cpf},{isvip},{conta}", async Task<IResult> (string nome,string cpf,bool isvip,int conta,IServiceCLient service) =>
        {
        bool resultado=  await  service.AddService(nome, cpf, conta, isvip);
        if (resultado)
        {
          return  Results.Ok("adicionado com sucesso");
        }
        return Results.BadRequest("erro ao adicionar");
        
        });
    }
}