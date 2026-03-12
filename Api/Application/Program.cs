

namespace api
{
    

    
    class Exec()
    {

        public static async Task Main()
        {
         
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            AddScope N1 = new();
            
            N1.AddScopeFuncion(builder);
            
            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
             await Routers.RoutersFuncionario.Router(app);
             
            app.Run();
            
        }
    }
}

