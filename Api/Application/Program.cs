

namespace api
{
    

    
    class Exec()
    {

        public static async Task Main()
        {
         
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            AddScope scope = new();
            
            scope.AddScopeFuncion(builder);
            
            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            Routers.routersMain.Teste(app);
           
           app.Run();
            
        }
    }
}

