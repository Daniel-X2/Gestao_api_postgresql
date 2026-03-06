



public class InvalidCpfException : Exception
{
    public InvalidCpfException() {}

    public InvalidCpfException(string cpf) : base($"o Cpf {cpf} nao e valido") { }
}

public class InvalidNameException : Exception
{
    public InvalidNameException(){}
    
    public InvalidNameException(string name):base($"o nome {name} nao e valido"){}
}

public class ErroAddToDatabaseException : Exception
{
    public ErroAddToDatabaseException(){}
    public ErroAddToDatabaseException(string localerro):base($"O ERRO ACONTECEU NO {localerro}"){}
}

public class ReturnDataIsEmpty : Exception
{
    public ReturnDataIsEmpty(){}
    public ReturnDataIsEmpty(string localErro) : base($"erro no {localErro}"){}
}

public class InvalidAccount : Exception
{
    public InvalidAccount(){}
    public InvalidAccount(int account):base($"a conta nao e valida {account}"){}
}

public class InvalidNascimentoException : Exception
{
    public InvalidNascimentoException(){}

    public InvalidNascimentoException(int nascimento) : base($"o ano de nascimento nao e valido "){}
}