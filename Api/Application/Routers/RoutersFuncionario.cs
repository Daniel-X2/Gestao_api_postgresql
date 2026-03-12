using Api.core.Application.service;

using Dto;


namespace api.Routers;

public class RoutersFuncionario
{
        
        public static async  Task Router(WebApplication app)
        {
            
            app.MapGet("/funcionario/get",async Task<ListaFuncionario> (IServiceFuncionario n1) =>
            {
              
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
            app.MapPost("/funcionario/add/{nome},{cpf},{isadmin},{quantidadeAtestado},{nascimento}", 
                    async Task<IResult> (string nome,string cpf, bool isadmin, 
                    int quantidadeAtestado,int nascimento, IServiceFuncionario service) =>
                    {
                     bool resultado=  await service.AddService(nome,cpf,quantidadeAtestado,nascimento, isadmin);
                     if (resultado)
                     {
                       return  Results.Ok("adicionado com sucesso");
                     }

                     return Results.BadRequest("erro ao adicionar");
                     
                    });
            
            
        }
        
        
}