using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Models;
using TicTacToe.API.ViewModels;

namespace TicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginViewModel loginViewModel)
        {
            var user = await _userService.Login(loginViewModel.Email, loginViewModel.Password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> Register(LoginViewModel loginModel)
        {
            var user = _mapper.Map<User>(loginModel);
            await _userService.Register(user);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return Ok(userViewModel);
        }


        [HttpGet("test")]
        public async Task<ActionResult<UserViewModel>> Test(LoginViewModel loginModel)
        {
            
            return Ok(DateTime.UtcNow.ToString());
        }
    }
}