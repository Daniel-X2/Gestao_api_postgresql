using Api.core.Application.service;

using Dto;


namespace api.Routers;

public class RoutersFuncionario
{
        
        public static async  Task Router(WebApplication app)
        {
            
            app.MapGet("/funcionario/get",async Task<ListaFuncionario> (IServiceFuncionario n1) =>
            {
              //e bom retornar com id pra se caso eu quiser deletar depois
                var n2= await n1.GetAll();
                return n2 ;
                
            });
            
        
            app.MapGet("/funcionario/get/{id}",async (int id,IServiceFuncionario n1) =>
            {
                var campo =await n1.GetByIdService(id);
                return campo;
            });
            app.MapDelete("/Funcionario/delete/{id}", async Task<IResult> (int id,IServiceFuncionario n1) =>
            {
                bool resultado =await n1.DeleteFuncionarioService(id);
              
                if (resultado)
                {
                    return  Results.Ok();
                }
                return  Results.BadRequest();
            });
            app.MapPost("/funcionario/add/",async Task<IResult> (FuncionarioDto campos, IServiceFuncionario service) =>
                    {
                     bool resultado=  await service.AddService(campos);
                     if (resultado)
                     {
                       return  Results.Ok("adicionado com sucesso");
                     }

                     return Results.BadRequest("erro ao adicionar");
                     
                    });
            app.MapPut("funcionario/update/{id}/", async Task<IResult> (int id,FuncionarioDto campos, IServiceFuncionario service) =>
            {
            bool resultado=  await  service.UpdateFuncionarioService(campos, id);
            if (resultado)
            {
                return  Results.Ok("atualizado com sucesso");
            }
            return Results.BadRequest("erro ao atualizar");
            });

        }
        
        
}