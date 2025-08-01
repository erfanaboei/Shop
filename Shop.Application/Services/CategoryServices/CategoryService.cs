using Shop.Application.IServices.ICategoryServices;
using Shop.Application.Mappings;
using Shop.Application.Mappings.CategoryMappings;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.CategoryDataTransferObjects;
using Shop.Domain.IRepositories;
using Shop.Domain.Models.Categories;

namespace Shop.Application.Services.CategoryServices
{
    public class CategoryService : Service<Category, CategoryDto>, ICategoryService, IScopedDependency
    {
        public CategoryService(IRepository<Category> repository) : base(repository)
        {
        }
    }
}