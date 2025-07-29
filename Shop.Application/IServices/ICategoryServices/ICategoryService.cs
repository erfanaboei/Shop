using Shop.Domain.DataTransferObjects.CategoryDataTransferObjects;
using Shop.Domain.Models.Categories;

namespace Shop.Application.IServices.ICategoryServices
{
    public interface ICategoryService : IService<Category, CategoryDto>
    {

    }
}