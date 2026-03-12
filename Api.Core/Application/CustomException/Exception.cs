


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

public class  ReturnDataIsEmpty : Exception
{
    //public ReturnDataIsEmpty(){}

    public ReturnDataIsEmpty() { }
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

public class InvalidIdException : Exception
{
    public InvalidIdException(int id):base($"O Id {id} Nao corresponde a nenhum indice"){}
}

public class InvalidConnection: Exception
{
    public InvalidConnection() : base("A conexao foi falha "){}
}

public class InvalidCodeException : Exception
{
    public InvalidCodeException(int codigo):base($"o codigo inserido {codigo} ja existe ou e invalido"){}
}

public class InvalidQuantityException : Exception
{
    public  InvalidQuantityException():base("a quantidade inserida e invalida"){}
}

public class InvalidLoteException : Exception
{
    public InvalidLoteException(int lote) : base($"o lote {lote} ja existe ou e um numero negativo"){}
}