
using Utils;
using Dto;
using Api.core.Application.repository;

namespace Api.core.Application.service
{
    public interface IServiceCLient
    {
        Task<ListaClient> GetAllService();

        Task<bool> UpdateService(int id, string nome = null, string cpf = null, int conta = 0,
            bool isvip = false);
        Task<bool> AddService(string nome, string cpf, int conta, bool isvip);
        Task<bool> DeleteService(int id);
    }
    class ClientService(IRepositoryClient repo):IServiceCLient
{
    public async Task<ClientDto> GetByIdService(int id)
    {
        ClientDto resultado= await repo.GetById(id);
        if (string.IsNullOrWhiteSpace(resultado.Nome))
        {
            throw new ReturnDataIsEmpty();
        }
        return resultado;
    }
    public async Task<ListaClient> GetAllService()//
    {
    
        
        ListaClient valores= await repo.GetAllClient();
        if(valores.lista_client.Count==0)
        {
            throw new ReturnDataIsEmpty();
        }
        
        return  valores;
    }
    public async Task<bool> AddService(string nome,string cpf,int conta, bool isvip)//
    {
        ClientDto campos=new();
        Validation verificador = new();
        
        campos.Nome = nome;
        campos.Isvip = isvip;
        campos.Conta = conta;
        try
        {
            verificador.VerificarNome(nome);
            campos.Cpf = verificador.IsValidDigit(cpf);
            await IsValidAccount(conta);
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
        catch (InvalidAccount)
        {
            return false;
        }
        int resultado= await repo.AddClient(nome,cpf,conta,isvip);
        if (resultado ==0)
        {
             throw new ErroAddToDatabaseException("AddService");
        }
        return true;
    }

    public async Task<bool> UpdateService(int id, string nome = null, string cpf = null, int conta = 0,
        bool isvip = false)
    {
        ClientDto campos = new();
        Validation verificar = new();
        //
        var valores =await  repo.GetById(id);
        if (string.IsNullOrWhiteSpace(valores.Nome))
        {
            throw new InvalidIdException(id);
        }
        try
        {
            verificar.IsValidDigit(cpf);

            if (await repo.IsExistsCpf(cpf))
            {
                campos.Cpf = cpf;
            }
            
        }
        catch (InvalidCpfException)
        {
            campos.Cpf = valores.Cpf;
        }
        if(!verificar.VerificarNome(nome)){
  
            campos.Nome = nome;
        }



        if (!await IsValidAccount(conta))
        {
            campos.Conta =conta;
        }
           
           
        campos.Isvip = isvip;
       
        if (await repo.UpdateClient(campos, id)==0)
        {
          throw new ErroAddToDatabaseException();
        }
        return true;
    }

    public async Task<bool> DeleteService(int id)//
    {
      if ( await repo.DeleteClient(id)==0)
      {
          throw new InvalidIdException(id);
      }
      return true;
    }

    public async Task<bool> IsValidAccount(int account)
    {
        if (int.IsNegative(account))
        {
            return false;
        }
        if(await repo.ContaExiste(account))
        {
            return false;
        }
        return true;
    }

    public async Task IsValidCpf(string cpf)
    {
        Validation verificador = new();
        verificador.IsValidDigit(cpf);
        if (await repo.IsExistsCpf(cpf) )
        {
            throw new InvalidCpfException(cpf);
        }
        
    }
    public async Task<int> GetIdService(string cpf)
    {
       int id= await repo.GetIdByCpf(cpf);
       return id;
    }
}
}