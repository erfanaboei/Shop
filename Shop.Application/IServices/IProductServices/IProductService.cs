using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;
using Shop.Domain.Models.Products;

namespace Shop.Application.IServices.IProductServices
{
    public interface IProductService : IService<Product, ProductDto>
    {
        
    }
}