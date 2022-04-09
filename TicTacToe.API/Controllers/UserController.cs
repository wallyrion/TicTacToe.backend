using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.API.ViewModels.User;
using TicTacToe.BLL.Dto.User;
using TicTacToe.BLL.Services.Interfaces;

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
        public async Task<ActionResult<UserVM>> Login(LoginRequestVM loginRequestVM)
        {
            var user = await _userService.Login(loginRequestVM.Email, loginRequestVM.Password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserVM>(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserVM>> Register(LoginRequestVM loginModel)
        {
            var loginDto = _mapper.Map<LoginDto>(loginModel);
            var user = await _userService.Register(loginDto);

            var userVM = _mapper.Map<UserVM>(user);

            return Ok(userVM);
        }


        [HttpGet("test")]
        public Task<ActionResult<UserVM>> Test()
        {
            return Task.FromResult<ActionResult<UserVM>>(Ok(DateTime.UtcNow.ToString()));
        }
    }
}