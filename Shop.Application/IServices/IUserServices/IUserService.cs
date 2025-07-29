using System.Threading;
using System.Threading.Tasks;
using Shop.Application.Utilities;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;
using Shop.Domain.Models.Users;

namespace Shop.Application.IServices.IUserServices
{
    public interface IUserService : IService<User, UserDto>
    {
        Task<RequestResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(int userId, CancellationToken cancellationToken);
    }
}