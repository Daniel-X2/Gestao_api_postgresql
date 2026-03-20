using System.Text;
using Utils;
using Dto;
using Api.Core.Application.repository;

namespace Api.Core.Application.service
{
    public interface IServiceCLient
    {
        Task<ListaClient> GetAllService();

        Task<StringBuilder> UpdateService(int id, ClientDto campos);
        Task<bool> AddService(ClientDto campos);
        Task<bool> DeleteService(int id);
    }
    class ServiceClient(IRepositoryClient repo):IServiceCLient
{
    public async Task<ClientDto> GetByIdService(int id)
    {
        
            ClientDto resultado = await repo.GetById(id);
            if (resultado==null)
            {
                throw new InvalidIdException(id);
            }
            return resultado;
 
    }
    public async Task<ListaClient> GetAllService()//
    {
    
        
        ListaClient valores= await repo.GetAllClient();
       
        switch (valores.Clients.Count)
        {
            case 0:
            {
                throw new ReturnDataIsEmpty();
            }
            case >= 1:
            {
                return valores;
            }
            default:
            {
                throw new ReturnDataIsEmpty();
            }
        }
   
    }
    public async Task<bool> AddService(ClientDto campos)//
    {
       
        Validation verificador = new();

        campos.Cpf = verificador.IsValidDigit(campos.Cpf);
        await IsValidAccount(campos.Conta);
        if (await repo.ExistsCpf(campos.Cpf))
        {
            throw new InvalidCpfException(campos.Cpf);
        }
        if (!verificador.ValidateName(campos.Nome))
        {
            throw new InvalidNameException();
        }
        int resultant= await repo.AddClient(campos);
        if (resultant ==0)
        {
             throw new ErroAddToDatabaseException("AddService");
        }
        return true;
    }

    public async Task<StringBuilder> UpdateService(int id,
        ClientDto campos)
    {
        StringBuilder camposNotUpdate=new ();
        Validation verificar = new();
        var valores =await  repo.GetById(id);
        
        if (valores==null)
        {
            throw new InvalidIdException(id);
        }

        try
        {
            campos.Cpf= verificar.IsValidDigit(campos.Cpf);
            if (await repo.ExistsCpf(campos.Cpf))
            {
                campos.Cpf = valores.Cpf;
                camposNotUpdate.Append(" CPF") ;
            }
        }
        catch (InvalidCpfException)
        {
            campos.Cpf = valores.Cpf;
            camposNotUpdate.Append(" CPF");
        }
      
        
        if(!verificar.ValidateName(campos.Nome) || campos.Nome==valores.Nome){
  
            campos.Nome = valores.Nome;
            camposNotUpdate.Append(" NOME");
        }
        
        try
        {
            await IsValidAccount(campos.Conta);

        }
        catch (InvalidAccountException)
        {
            campos.Conta =valores.Conta;
            camposNotUpdate.Append(" CONTA");
        }
        

        switch (await repo.UpdateClient(campos, id))
        {
            case 0:
            {
                throw new ErroUpdateToDatabaseException();
            }
            case >= 1:
            {
                return camposNotUpdate;
            }
        }

        return camposNotUpdate;





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
        if (int.IsNegative(account) || account==0)
        {
            throw new InvalidAccountException();
        }
        if(await repo.ExistsAccount(account))
        {
            throw new InvalidAccountException();
        }
        return true;
    }

    public async Task IsValidCpf(string cpf)
    {
        Validation verificador = new();
        verificador.IsValidDigit(cpf);
        if (await repo.ExistsCpf(cpf) )
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