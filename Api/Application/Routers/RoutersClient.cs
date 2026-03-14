using Api.core.Application.service;
using Dto;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sprache;

namespace api.Routers;

public class RoutersClient
{
    public static async Task Routers(WebApplication app)
    {
        app.MapGet("/client/get/", async Task<ListaClient> (IServiceCLient service) =>
        {
         ListaClient lista=  await service.GetAllService();
         
         return lista ;
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
        app.MapPost("client/add/", async Task<IResult> (ClientDto campos,IServiceCLient service) =>
        {
        bool resultado=  await  service.AddService(campos);
        if (resultado)
        {
          return  Results.Ok("adicionado com sucesso");
        }
        return Results.BadRequest("erro ao adicionar");
        });
        app.MapPut("client/update/{id}/", async Task<IResult> (int id, ClientDto campos, IServiceCLient service) =>
        {
          bool resultado= await service.UpdateService(id,campos);
          if (resultado)
          {
              return  Results.Ok("atualizado com sucesso");
          }

          return Results.BadRequest("erro ao atualizar");
        });
        
        

    }
}