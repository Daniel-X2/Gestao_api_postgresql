namespace api.Routers;

public class routersMain
{
    public async static Task Teste(WebApplication app)
    {
       RoutersClient.Routers(app);
       RoutersFuncionario.Router(app);
       RoutersProduct.Routers(app);
       
    }
}