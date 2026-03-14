using Api.core.Application.repository;
using Dto;
using Utils;

public interface IServiceProduct
{
  public  Task<ListaProduto> GetAllProduct();
  public Task<bool> AddProduct(ProdutoDto campos);
  public Task<bool> DeleteProduct(int id);
  public Task<ListaProduto>  GetEstoque();
  public Task<List<float>> GetValorBruto();
  public Task<ProdutoDto> GetProdutId(int id);
  public Task UpdateProduct(ProdutoDto campos, int id);
}
class ServiceProduct(IRepositoryProduct repo):IServiceProduct
{
    public async Task<ListaProduto> GetAllProduct()
    {
       
       ListaProduto  lista =await repo.GetAllProduct();
       if (lista.lista_prod.Count==0)
       {
           throw new ReturnDataIsEmpty();
       }

       return lista;
    }

    public async Task<bool> AddProduct(ProdutoDto campos)
    {
        
            var validation = new Validation();
            if (!validation.VerificarNome(campos.Nome))
            {
                throw new InvalidNameException();
            }
            if (await repo.IsExistCode(campos.Codigo) || int.IsNegative(campos.Codigo))
            {
                throw new InvalidCodeException(campos.Codigo);
            }
            if (campos.Quantidade<=0)
            {
                throw new InvalidQuantityException();
            }

            if (campos.ValorRevenda<=0)
            {
                throw new ArgumentException("aqui ta ruim");
            }

            if (await repo.IsExistLote(campos.Lote) || campos.Lote<=0)
            {
                throw new InvalidLoteException(campos.Lote);
            }

         
            

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