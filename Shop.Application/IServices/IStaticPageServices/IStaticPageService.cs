using Shop.Domain.DataTransferObjects.StaticPageDataTransferObjects;
using Shop.Domain.Models.StaticPages;

namespace Shop.Application.IServices.IStaticPageServices
{
    public interface IStaticPageService: IService<StaticPage, StaticPageDto>
    {
        
    }
}