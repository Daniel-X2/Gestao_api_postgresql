using Api.Core.Application.service;
using Api.Test;
using Dto;



namespace Api.Routers;

public class  ClientRouter
{
    
    
    public  async  Task Routers(WebApplication app)
    {
        app.MapGet("/ola", async Task(IServiceClient service,IServiceFuncionario servicefun,IServiceProduct servicprdo) =>
        {
            while (true)
            {
                await  service.AddService(ReturnDados.ReturnCLient());
                await servicefun.AddService(ReturnDados.ReturnFuncionario());
                await servicprdo.AddProduct(ReturnDados.ReturnProduct());
            }
        });
        app.MapGet("/client/get/", async Task<ListaClient> (IServiceClient service) =>
        {
           
            ListaClient lista=  await service.GetAllService();
            
            return lista ;
        });
        app.MapDelete("client/delete/{id}", async  Task<IResult> (int id,IServiceClient service) =>
        {
          bool resultado= await service.DeleteService(id);
          //await _next(context);
          if (resultado)
          {
              
            
              return Results.Ok(new {resultado="foi deletado com sucesso"});
          }
          return Results.BadRequest(new {erro="erro ao tentar deletar"});
          
          
        });
        app.MapPost("client/add/", async Task<IResult> (ClientDto campos,IServiceClient service) =>
        {
        bool resultado=  await  service.AddService(campos);
     
        
        if (resultado)
        {
            return Results.Ok(new {resultado="foi adicionado com sucesso"});
        }
        return Results.BadRequest(new {erro="erro ao tentar adicionar"});
      
        });
        app.MapPut("client/update/{id}/", async Task<IResult> (int id, ClientDto campos, IServiceClient service) =>
        {
          var resultado= await service.UpdateService(id,campos);
          //_next(context);

          if (resultado.Length==0 )
          {
              return Results.Ok(new {resultado=$"atualizado com sucesso {resultado.Length}"});
          }
          return Results.Ok(new {resultado="teve atualizaçoes parciais",camposNotUpdate=$"campos nao atualizados {resultado}"});
          
        });
        
        

    }
}