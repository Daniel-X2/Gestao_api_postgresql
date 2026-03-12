using Api.core.Application.service;
using Api.core.Application.utils;
using Api.core.Application.repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Routers;

public class RoutersFuncionario(WebApplication app)
{
        
        public async  Task Router()
        {
            
            app.MapGet("/funcionario/get",async (IServiceFuncionario n1) =>
            {
                
                var n2= await n1.GetAll();
                return n2.lista_funci ;
                
            });
            
        
            app.MapGet("/funcionario/get/{id}",async (int id,IServiceFuncionario n1) =>
            {
                var campo =await n1.GetByIdService(id);
                return campo;
            });
            app.MapDelete("/Funcionario/delete/{id}", async Task<string> (int id,IServiceFuncionario n1) =>
            {
                bool resultado =await n1.DeleteFuncionarioService(id);
                return "aaaaa";
                if (resultado)
                {
                    //return new OkObjectResult("55");
                }

                // return new BadRequestResult();
            });
        }
        
        
}