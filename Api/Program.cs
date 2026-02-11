

using static System.Console;




namespace api
{
    

    class Routers
    {
        
        public static void Router_Home(WebApplication app)
        {
            
            app.MapGet("/", () => 
            {
               
                return "";
                
            });
            
        
            app.MapGet("/oi", () =>
            {
                WriteLine("alo");
                return "outro";
            });
        }
        
    }
    class Exec:repository_client
    {

        public static void aMain()
        {
            
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            var app = builder.Build();
            Routers.Router_Home(app);
            app.Run();
            
            
            
        }
    }
}

