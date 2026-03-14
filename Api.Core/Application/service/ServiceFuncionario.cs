using Dto;
using Utils;
using Api.core.Application.repository;
namespace Api.core.Application.service
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
        //colocar no Iservice
       public async Task<ListaFuncionario> GetAll()
       {
           ListaFuncionario lista=new();
           lista =await repo.GetFuncionario();
           if (lista.lista_funci.Count==0)
           {
               throw new ReturnDataIsEmpty();
           }

           return lista;
       }

        public async Task<bool> AddService(FuncionarioDto campos)
       {


          
           Validation verificador = new();
           //campos.Cpf = cpf;
           
           try
           {
               verificador.VerificarNome(campos.Nome);
               verificador.IsValidNascimento(campos.Nascimento);
               campos.Cpf = verificador.IsValidDigit(campos.Cpf);
               if (await repo.IsExistsCpf(campos.Cpf))
               {
                   throw new InvalidCpfException(campos.Cpf);
               }
           }
           catch (InvalidNameException)
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

            if (!verificar.IsValidNascimento(campos.Nascimento))
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

