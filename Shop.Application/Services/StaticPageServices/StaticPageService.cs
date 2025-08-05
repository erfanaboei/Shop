using Shop.Application.IServices.IStaticPageServices;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.StaticPageDataTransferObjects;
using Shop.Domain.IRepositories.IStaticPageRepositories;
using Shop.Domain.Models.StaticPages;

namespace Shop.Application.Services.StaticPageServices
{
    public class StaticPageService:Service<StaticPage, StaticPageDto>, IStaticPageService, IScopedDependency
    {
        public StaticPageService(IStaticPageRepository repository) : base(repository)
        {
        }
    }
}