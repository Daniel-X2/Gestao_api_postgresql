using Dto;
using Utils;
using Api.Core.Application.repository;
namespace Api.Core.Application.service
{
   public interface IServiceFuncionario
   {
       public Task<bool> AddService(FuncionarioDto campos);

      public Task<bool> UpdateFuncionarioService(FuncionarioDto campos, int id);
      public Task<bool> DeleteFuncionarioService(int id); 
      public Task<ListaFuncionario> GetAll();
      public Task<FuncionarioDto> GetByIdService(int id);
   }
    public class  ServiceFuncionario(IRepositoryFuncionario repo):IServiceFuncionario
    {
       
       public async Task<ListaFuncionario> GetAll()
       {
           ListaFuncionario lista=new();
           lista =await repo.GetFuncionario();
           if (lista.Funcionarios.Count==0)
           {
               throw new ReturnDataIsEmpty();
           }

           return lista;
       }

        public async Task<bool> AddService(FuncionarioDto campos)
       {
           Validation verificador = new();
           
           verificador.IsValidNascimento(campos.Nascimento);//aqui retorna bool
           campos.Cpf = verificador.IsValidDigit(campos.Cpf);
           if (verificador.VerificarNome(campos.Nome))
           {
               throw new InvalidNameException(campos.Nome);
           }
           if (await repo.IsExistsCpf(campos.Cpf))
           {
               throw new InvalidCpfException(campos.Cpf);
           }

           int resultado = await repo.AddFuncionario(campos);
           switch (resultado)
           {
               case 0:
               {
                   throw new ErroAddToDatabaseException();
               }
               case 1:
               {
                   return true;
               }
               default:
               {
                   return false;
               }
           }
       }

        public async Task<bool> UpdateFuncionarioService(FuncionarioDto campos,int id)
        {
            
            Validation verificar = new();
            // devo fazer um return dos campos que nao foram atualizados
            var valores =await  repo.GetById(id);
            if (string.IsNullOrWhiteSpace(valores.Nome))
            {
                throw new InvalidIdException(id);
            }

            try
            {
                campos.Cpf= verificar.IsValidDigit(campos.Cpf);
                if ( await repo.IsExistsCpf(campos.Cpf))
                {
                    campos.Cpf = valores.Cpf;
                }
            }
            catch (InvalidCpfException)
            {
                campos.Cpf = valores.Cpf;

            }

            if (!verificar.VerificarNome(campos.Nome))
            {
                 campos.Nome = valores.Nome;
            }

            try
            {
                if (!verificar.IsValidNascimento(campos.Nascimento))
                {
                    campos.Nascimento = valores.Nascimento;
                }
            }
            catch (InvalidNascimentoException)
            {
                campos.Nascimento = valores.Nascimento;
            } 
            campos.Isadmin = valores.Isadmin;
       
            if (await repo.UpdateFuncionario(campos, id)==0)
            {
                throw new ErroUpdateToDatabaseException();
            }
            return true;
        }
    
        public async Task<bool> DeleteFuncionarioService(int id)
        {
            switch (await repo.DeleteFuncionario(id))
            {
                case 0:
                {
                    throw new InvalidIdException(id);
                }
                case >= 1:
                {
                    return true;
                }
                default:
                {
                    throw new InvalidIdException(id);
                }
            }
            
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

