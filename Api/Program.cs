using Api.Core.Application.utils;
namespace Api
{
    

    
    class Exec
    {
        
        
        public static async Task Main()
        {
            
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            AddScope scope = new();
            Load.LoadEnv();
           
            scope.AddScopeFuncion(builder);
            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            await new Routers.Routers().Teste(app);
            app.UseHttpsRedirection();
            
            
           
             app.Run();
            
        }
    }
}

