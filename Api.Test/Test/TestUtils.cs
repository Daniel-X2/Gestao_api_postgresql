using Api.Test;
using Xunit;

using Utils;

public class TestUtils
{
    private static Validation validation=new ();
    
    [Fact]
    public  void TestCpfValido()
    {
        string cpf = ReturnDados.ReturnCpf();
     
        cpf= validation.IsValidDigit(cpf);
        Assert.Equal(11,cpf.Length);
        Assert.Equal(cpf,cpf);
        
       
    }
    [Fact]
    public  void TestCpfInvalido()
    {
        string cpf = "78546245501";
        Assert.Throws<InvalidCpfException>(() =>  validation.IsValidDigit(cpf));
        

    }
    
    [Theory]
    [InlineData(2200)]
    [InlineData(1500)]
    public  void TestNascimentoInvalido(int nascimento)
    {
        Assert.Throws<InvalidNascimentoException>(() => validation.ValidateBirthYear(nascimento));
        
    }
    [Fact]
    public void TestNascimentoValido()
    {
           bool resultado= validation.ValidateBirthYear(ReturnDados.ReturnBirthYear());
           Assert.True(resultado);
    }
    
    
}