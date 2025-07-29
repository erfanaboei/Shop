using System.Threading.Tasks;
using Shop.Domain;
using Shop.Domain.Models.Users;

namespace Shop.Application.IServices
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}