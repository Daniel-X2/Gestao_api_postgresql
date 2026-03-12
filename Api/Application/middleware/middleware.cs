
public class ExceptionHandlingMiddleware
{
    private  RequestDelegate _next;

    public  ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ReturnDataIsEmpty)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidCpfException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidNameException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (ErroAddToDatabaseException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidAccount)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidNascimentoException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidIdException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidConnection)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidCodeException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidQuantityException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
        catch (InvalidLoteException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "errrroooo aquiii" });
        }
    }
}