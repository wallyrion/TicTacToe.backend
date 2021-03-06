using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.API.ViewModels.User;
using TicTacToe.BLL.Dto.User;
using TicTacToe.BLL.Services.Interfaces;

namespace TicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly ITokenService _tokenService;
        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper,
            ITokenService tokenService
            )
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserVM>> Login(LoginRequestVM loginRequestVM)
        {
            var user = await _userService.Login(loginRequestVM.Email, loginRequestVM.Password);

            Tuple<string, string> tokens = await _tokenService.GenerateTokensAsync(user.Id);

            var userVM = _mapper.Map<UserVM>(user);

            var response = new TokenResponseVM(userVM, tokens.Item1, tokens.Item2);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponseVM>> Register(LoginRequestVM loginModel)
        {
            var loginDto = _mapper.Map<LoginDto>(loginModel);
            var user = await _userService.Register(loginDto);

            var userVM = _mapper.Map<UserVM>(user);
            var tokens = await _tokenService.GenerateTokensAsync(user.Id);

            var response = new TokenResponseVM(userVM, tokens.Item1, tokens.Item2);
            return Ok(response);
        }


        [HttpPost("refresh_token")]
        public async Task<IActionResult> RefreshToken([Required]RefreshTokenRequestVM refreshTokenRequest)
        {
            var refreshTokenRequestDto = _mapper.Map<RefreshTokenRequestDto>(refreshTokenRequest);
            await _tokenService.ValidateRefreshTokenAsync(refreshTokenRequestDto);

            var tokenResponse = await _tokenService.GenerateTokensAsync(refreshTokenRequestDto.UserId);

            return Ok(new RefreshTokensResponseVM(tokenResponse.Item1, tokenResponse.Item2));
        }

        [Authorize]
        [HttpGet("my-info")]
        public async Task<ActionResult<UserVM>> GetCurrentUserInfo()
        {
            var user = await _userService.GetUser(UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userVM = _mapper.Map<UserVM>(user);
            return Ok(userVM);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<List<UserVM>>> Search(string part, CancellationToken token)
        {
            var usersDto = await _userService.Search(part, UserId, token);

            return _mapper.Map<List<UserVM>>(usersDto);
        }


        [HttpGet("test")]
        public Task<ActionResult<UserVM>> Test()
        {
            return Task.FromResult<ActionResult<UserVM>>(Ok(DateTime.UtcNow.ToString()));
        }
    }
}