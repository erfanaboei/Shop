using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Application.IServices;
using Shop.Application.IServices.IUserServices;
using Shop.Application.Mappings;
using Shop.Application.Mappings.UserMappings;
using Shop.Application.Utilities;
using Shop.Domain;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;
using Shop.Domain.IRepositories.IUserRepositories;
using Shop.Domain.Models.Users;

namespace Shop.Application.Services.UserServices
{
    public class UserService : Service<User, UserDto>, IUserService, IScopedDependency
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public UserService(UserMapper mapper, IUserRepository userRepository, IJwtService jwtService, UserManager<User> userManager) : base(userRepository ,mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public override async Task<RequestResult<UserDto>> AddAsync(UserDto dto, CancellationToken cancellationToken)
        {
            if (await IsExistAsync(r => r.Email == dto.Email, cancellationToken))
            {
                return new RequestResult<UserDto>(false, RequestResultStatusCode.Conflict, null,  "ایمیل وارد شده تکراری است!");
            }

            if (await IsExistAsync(r=> r.PhoneNumber == dto.PhoneNumber, cancellationToken))
            {
                return new RequestResult<UserDto>(false, RequestResultStatusCode.Conflict, null, "شماره موبایل وارد شده تکراری است!");
            }

            if (await IsExistAsync(r=> r.UserName.ToLower() == dto.UserName.ToLower(), cancellationToken))
            {
                return new RequestResult<UserDto>(false, RequestResultStatusCode.Conflict, null, "نام کاربری وارد شده از قبل انتخاب شده است!");
            }
            
            dto.Code = Guid.NewGuid().ToString();
            dto.CreateDate = DateTime.Now;
            dto.SecurityStamp = Guid.NewGuid().ToString();
            
            return await base.AddAsync(dto, cancellationToken);
        }

        public async Task<RequestResult> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByQueryAsync(r=> r.UserName == dto.UserName && r.PasswordHash == SecurityHelper.GetSha256Hash(dto.Password), cancellationToken);

            if (user is null)
                return new RequestResult(false, RequestResultStatusCode.NotFound, "کاربری با این مشخخصات یافت نشد!");

            var token = await _jwtService.GenerateAsync(user);
            await UpdateLastLoginDateAsync(user.Id, cancellationToken);
            
            return new RequestResult(true, RequestResultStatusCode.Success, additionalData:token);
        }

        public async Task UpdateLastLoginDateAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await GetByIdAsync(userId, cancellationToken);
            user.LastLoginDate = DateTime.Now;
            await UpdateByModelAsync(user, cancellationToken);
        }
    }
}