using System.Linq;
using Shop.Domain.DataTransferObjects.GeneralDataTransferObjects;
using Shop.Domain.DataTransferObjects.ProductDataTransferObjects;
using Shop.Domain.Models.Products;

namespace Shop.Application.Mappings.ProductMappings
{
    public class ProductVariantMapper : GenericMapper<ProductVariant, ProductVariantDto>
    {
        public override ProductVariantDto ToDto(ProductVariant model)
        {
            return new ProductVariantDto()
            {
                Id = model.Id,
                ProductId = model.ProductId,
                SKU = model.SKU,
                Price = model.Price,
                AttributeValueIds = model.ProductVariantAttributeValues.Select(r => r.AttributeValueId).ToList(),
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                DeleteDate = model.DeleteDate,
            };
        }

        public override ProductVariant ToModel(ProductVariantDto dto)
        {
            return new ProductVariant()
            {
                
                Id = dto.Id,
                ProductId = dto.ProductId,
                SKU = dto.SKU,
                Price = dto.Price,
                ProductVariantAttributeValues = dto.AttributeValueIds.Select(r => new ProductVariantAttributeValue()
                {
                    AttributeValueId = r
                }).ToList(),
                CreateDate = dto.CreateDate,
                UpdateDate = dto.UpdateDate,
                DeleteDate = dto.DeleteDate,
            };
        }

        public override OptionDto ToOption(ProductVariant model)
        {
            return new OptionDto()
            {
                Value = model.Id.ToString(),
                Text = $"{model.Product.Title} - {model.SKU}"
            };
        }
    }
}