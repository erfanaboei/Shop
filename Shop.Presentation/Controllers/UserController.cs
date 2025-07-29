using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.IServices.IUserServices;
using Shop.Application.Utilities;
using Shop.Domain.DataTransferObjects.UserDataTransferObjects;

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
            return new RequestResult(true, RequestResultStatusCode.Success);
        }
    }
}