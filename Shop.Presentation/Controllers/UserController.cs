using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IUserServices;
using Shop.Application.Mappings;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;
using Shop.Domain.Models.Users;

namespace Shop.Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<RequestResult<List<UserDto>>> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllDtoAsync(cancellationToken);
            return users;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<RequestResult<UserDto>> AddAsync(UserDto dto, CancellationToken cancellationToken)
        {
            return await _userService.AddAsync(dto, cancellationToken);
        }
        
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<RequestResult> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            return await _userService.LoginAsync(dto, cancellationToken);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<RequestResult> Temp()
        {
            // var dto = new UserDto()
            // {
            //     Id = 12,
            //     Address = "test",
            //     Code = "dajadsfjalskdfjalskd",
            //     Email = "test@gmail.com",
            //     Name = "test",
            //     Family = "test2",
            //     UserName = "test",
            // };
            //
            // var model = new User()
            // {
            //     CreateDate = DateTime.Now,
            //     UpdateDate = DateTime.Now,
            //     DeleteDate = DateTime.Now,
            //     Address = "Test,Test,Test,Test",
            // };
            //
            // model = dto.ToModel(model).ForMember(r=> r.Name, r=> $"{r.Name} {r.Family}").Map();
            // dto = model.ToDto(dto).ForMember(r=> r.UserName, r=> r.Code).Map();

            //var users = await _userService.GetAllAsync(CancellationToken.None);

            //var test = users.ToOptions(user => user.UserName, user => $"{user.Name} {user.Family}");
            //var test = users.ToOptions();
            
            return new RequestResult(true, RequestResultStatusCode.Success);
        }
    }
}