using Api.core.Application.repository;
using Dto;
using Microsoft.Extensions.Options;
using Utils;

interface IServiceProduct
{
  public  Task<ListaProduto> GetAll();
  public  Task<bool> AddProduct(string nome, int codigo, int quantidade, float valor_revenda, int lote);
  public Task<bool> DeleteProduct(int id);
  public Task<ListaProduto>  GetEstoque();
  public Task<List<float>> GetValorBruto();
  public Task<ProdutoDto> GetProdutId(int id);
  public Task UpdateProduct(ProdutoDto campos, int id);
}
class ServiceProduct(IRepositoryProduct repo):IServiceProduct
{
    public async Task<ListaProduto> GetAll()
    {
       
       ListaProduto  lista =await repo.GetAllProduct();
       if (lista.lista_prod.Count==0)
       {
           throw new ReturnDataIsEmpty();
       }

       return lista;
    }

    public async Task<bool> AddProduct(string nome,int codigo,int quantidade,float valor_revenda,int lote)
    {
        
            var validation = new Validation();
            if (!validation.VerificarNome(nome))
            {
                throw new InvalidNameException();
            }
            if (await repo.IsExistCode(codigo) || int.IsNegative(codigo))
            {
                throw new InvalidCodeException(codigo);
            }
            if (quantidade<=0)
            {
                throw new InvalidQuantityException();
            }

            if (valor_revenda<=0)
            {
                throw new ArgumentException("aqui ta ruim");
            }

            if (await repo.IsExistLote(lote) || lote<=0)
            {
                throw new InvalidLoteException(lote);
            }

            ProdutoDto campos = new();
            campos.Nome = nome;
            campos.Codigo = codigo;
            campos.Lote = lote;
            campos.Quantidade = quantidade;
            campos.ValorRevenda = valor_revenda;

            int resultado = await repo.AddProduct(campos);
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

    public async Task<bool> DeleteProduct(int id)
    {
        if (await repo.DeleteProduct(id)==0)
        {
            throw new InvalidIdException(id);
        }
        
        return true;
    }

    public async Task<ListaProduto> GetEstoque()
    {
        ListaProduto lista =await repo.GetEstoque();
        if (lista.lista_prod.Count <= 0)
        {
            throw new ReturnDataIsEmpty();
        }

        return lista;
    }

    public async Task<List<float>> GetValorBruto()
    {
        List<float> lista =await repo.GetValorBruto();

        if (lista.Count<=0)
        {
            throw new ReturnDataIsEmpty();
        }

        return lista;
    }

    public async Task<ProdutoDto> GetProdutId(int id)
    { 
        ProdutoDto product= await  repo.GetProductById(id);
        if (string.IsNullOrEmpty(product.Nome))
        {
            throw new ReturnDataIsEmpty();
        }
        return product;
    }

    public async Task UpdateProduct(ProdutoDto campos,int id)
    {
        Validation validation = new();
        var valores =await repo.GetProductById(id);
        
        if (string.IsNullOrWhiteSpace(valores.Nome))
        {
            throw new ReturnDataIsEmpty();
        }
        if (await repo.IsExistCode(campos.Codigo) || campos.Codigo<=0)
        {
            campos.Codigo = valores.Codigo;
            //aqui coloca o negocio pra listar os que nao foram atualizados
        }
            
        if (await repo.IsExistLote(campos.Lote) || campos.Lote<=0)
        {
            campos.Lote = valores.Lote;
        }
        
        if (!validation.VerificarNome(campos.Nome))
        {
            campos.Nome = valores.Nome;
        }
        
        if (campos.Quantidade<=0)
        {
            campos.Quantidade = valores.Quantidade;
        }

        if (campos.ValorRevenda<=0)
        {
            campos.ValorRevenda = valores.ValorRevenda;
        }

       int resultado= await repo.UpdateProduct(campos, id);
       switch (resultado)
       {
           case 0:
           {
               break;
           }
           case 1:
           {
               break;
           }
           default:
           {
               break;
           }
       }
    }
}