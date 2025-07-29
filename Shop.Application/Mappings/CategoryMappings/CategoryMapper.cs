using Shop.Domain.DataTransferObjects.CategoryDataTransferObjects;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.Models.Categories;

namespace Shop.Application.Mappings.CategoryMappings
{
    public class CategoryMapper : GenericMapper<Category, CategoryDto>
    {
        public override CategoryDto ToDto(Category model)
        {
            return new CategoryDto()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreateDate = model.CreateDate,
                ImageUrl = model.ImageUrl,
                IsActive = model.IsActive,
                ParentId = model.ParentId,
            };
        }

        public override Category ToModel(CategoryDto dto)
        {
            return new Category()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                CreateDate = dto.CreateDate,
                ImageUrl = dto.ImageUrl,
                IsActive = dto.IsActive,
                ParentId = dto.ParentId,
            };
        }

        public override OptionDto ToOption(Category model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = model.Name
            };
        }
    }
}