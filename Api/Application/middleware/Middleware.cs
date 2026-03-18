
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
        catch (ReturnDataIsEmpty )
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "nao teve correspondecia aos dados solicitados"});
        }
        catch (InvalidCpfException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { erro = "O Cpf inserido e invalido",details="Verifique o cpf e tente novamente"});
        }
        catch (InvalidNameException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { erro = "O nome inserido e invalido",details="nomes com menos de 4 caracteres nao e aceito" });
        }
        catch (ErroAddToDatabaseException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "aconteceu um erro ao adicionar os  dados",details=e.Message});
        }
        catch (InvalidAccountException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { erro = "A conta inserida e invalida",details="Verifique se a conta ja existe ou o numero e negativo" });
        }
        catch (InvalidNascimentoException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "O ano de nascimento nao e valido" });
        }
        catch (InvalidIdException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "o Id inserido e invalido" });
        }
        catch (InvalidConnection e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { erro = "aconteceu um erro de conexao",details=e.Message});
        }
        catch (InvalidCodeException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { erro = "O codigo inserido e invalido",details="o codigo ja existe ou o numero e negativo" });
        }
        catch (NegativeNumericException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            await context.Response.WriteAsJsonAsync(new { erro = "O numero inserido e negativo" });
        }
        catch (InvalidLoteException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "O numero de lote e invalido" });
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { erro = "aconteceu um erro inesperado",details=e.Message});
            
        }
    }
}