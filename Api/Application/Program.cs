

namespace api
{
    

    
    class Exec()
    {

        public static async Task Main()
        {
         
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            TESTE N1 = new();
            
            N1.TEaSTE(builder);
            var app = builder.Build();
            new Routers.RoutersFuncionario(app).RouterDelete();
            
            app.Run();
            
        }
    }
}

