using Dto;
using Utils;
using Api.core.Application.repository;
using System.Reflection;
namespace Api.core.Application.service
{
    public class  Service(IRepositoryFuncionario repo)
    {
        public async Task IsValidCpf(string cpf)
        {
            Validation verificador = new();
            verificador.IsValidDigit(cpf);
            if (await repo.IsExistsCpf(cpf) )
            {
                throw new InvalidCpfException(cpf);
            }
        
        }
        public async Task<bool> AddService(string nome,string cpf,int quantidadeAtestado,int nascimento,bool isadmin)
        {
            
            FuncionarioDto campos=new();
            Validation verificador = new();
            campos.Cpf = cpf;
            campos.Nome = nome;
            campos.QuantidadeAtestado = quantidadeAtestado;
            campos.Nascimento = nascimento;
            campos.Isadmin = isadmin;
            try
            {
                verificador.VerificarNome(nome);
                await IsValidCpf(cpf);
                verificador.IsValidNascimento(nascimento);

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
            //
            var valores =await  repo.GetById(id);
            if (string.IsNullOrWhiteSpace(valores.Nome))
            {
                throw new ReturnDataIsEmpty(MethodBase.GetCurrentMethod().Name);
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

        public async Task<bool> DeleteFuncionarioService()
        {
            
        }
    }
}

