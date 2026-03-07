using Dto;
using Utils;
using Api.core.Application.repository;
using System.Reflection;
namespace Api.core.Application.service
{
   public interface IServiceFuncionario
   {
       public Task<bool> AddService(string nome, string cpf, int quantidadeAtestado, int nascimento,
           bool isadmin);

       Task<bool> UpdateFuncionarioService(FuncionarioDto campos, int id);
       Task<bool> DeleteFuncionarioService(int id);
       
   }
    public class  ServiceFuncionario(IRepositoryFuncionario repo):IServiceFuncionario
    {
       
        public async Task<bool> AddService(string nome,string cpf,int quantidadeAtestado,int nascimento,bool isadmin)
        {
            
            
            FuncionarioDto campos=new();
            
            Validation verificador = new();
            //campos.Cpf = cpf;
            campos.Nome = nome;
            campos.QuantidadeAtestado = quantidadeAtestado;
            campos.Nascimento = nascimento;
            campos.Isadmin = isadmin;
            try
            { 
                verificador.VerificarNome(nome);
                verificador.IsValidNascimento(nascimento);
                campos.Cpf = verificador.IsValidDigit(cpf);
                if (await repo.IsExistsCpf(cpf))
                {
                    throw new InvalidCpfException(cpf);
                }
            }
            catch (InvalidNameException )
            {
                return false;
            }
            catch (InvalidCpfException)
            {
                //e bom criar um exception com https pra ele retornar direto os erros certos
                return false;
            }
            catch (InvalidNascimentoException)
            {
                return false;
            }
            int resultado= await repo.AddFuncionario(campos);
            if (resultado ==0)
            {
                throw new ErroAddToDatabaseException("AddService");
            }
            return true;
        }
        public async Task<bool> UpdateFuncionarioService(FuncionarioDto campos,int id)
        {
            
            //FuncionarioDto campos = new();
            Validation verificar = new();
            // devo fazer um return dos campos que nao foram atualizados
            var valores =await  repo.GetById(id);
            if (string.IsNullOrWhiteSpace(valores.Nome))
            {
                throw new InvalidIdException(id);
            }
            try
            {
                verificar.IsValidDigit(campos.Cpf);
                await repo.IsExistsCpf(campos.Cpf);
                
            }
            catch (InvalidCpfException)
            {
                campos.Cpf = valores.Cpf;
            }
            try
            {
                verificar.VerificarNome(campos.Nome);
                //campos.Nome = nome;
            }
            catch (InvalidNameException)
            {
                campos.Nome = valores.Nome;
            }

            try
            {
                verificar.IsValidNascimento(campos.Nascimento);
                //campos.Conta =conta;
            }
            catch (InvalidNascimentoException)
            {
                campos.Nascimento = valores.Nascimento;
            }

            campos.Isadmin = valores.Isadmin;
       
            if (await repo.UpdateFuncionario(campos, id)==0)
            {
                throw new ErroAddToDatabaseException();
            }
            return true;
        }
    
        public async Task<bool> DeleteFuncionarioService(int id)
        {
            if (await repo.DeleteFuncionario(id) == 0)
            {
                throw new InvalidIdException(id);
            }
            return true;
        }
        public async Task<FuncionarioDto> GetByIdService(int id)
        {
            FuncionarioDto resultado= await repo.GetById(id);
            if (string.IsNullOrWhiteSpace(resultado.Nome))
            {
                throw new ReturnDataIsEmpty();
            }
            return resultado;
        }
        
    }
}

