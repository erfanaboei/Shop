using System.Linq;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;
using Shop.Domain.Models.Products;

namespace Shop.Application.Mappings.ProductMappings
{
    public class ProductMapper : GenericMapper<Product, ProductDto>
    {
        public override ProductDto ToDto(Product model)
        {
            return new ProductDto()
            {
                Id = model.Id,
                Description = model.Description,
                Title = model.Title,
                Code = model.Code,
                Inventory = model.Inventory,
                Price = model.Price,
                DiscountPercentage = model.DiscountPercentage,
                CreateDate = model.CreateDate,
                CategoryIds = model.ProductCategories?.Select(r=> r.CategoryId).ToList(),
                ImageUrls = model.ProductImages?.Select(r=> r.ImageUrl).ToList()
            };
        }

        public override Product ToModel(ProductDto dto)
        {
            return new Product()
            {
                Id = dto.Id,
                Description = dto.Description,
                Title = dto.Title,
                Code = dto.Code,
                Inventory = dto.Inventory,
                Price = dto.Price,
                DiscountPercentage = dto.DiscountPercentage,
                CreateDate = dto.CreateDate,
                ProductCategories = dto.CategoryIds?.Select(r=> new ProductCategory() { CategoryId = r }).ToList(),
                ProductImages = dto.ImageUrls?.Select(r=> new ProductImage() { ImageUrl = r }).ToList()
            };
        }

        public override OptionDto ToOption(Product model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = $"{model.Title} - {model.Code}"
            };
        }
    }
}