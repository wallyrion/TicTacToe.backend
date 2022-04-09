using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.API.ViewModels.Game;
using TicTacToe.BLL.Dto;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Services.Interfaces;
using TicTacToe.BLL.SignalR;

namespace TicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        public GameController(
            ILogger<GameController> logger,
            IGameService gameService,
            IMapper mapper)
        {
            _logger = logger;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost("Invite")]
        public async Task<ActionResult<GameInvitationDto>> InviteToGame(GameInviteViewModel viewModel)
        {
            var gameDto = _mapper.Map<GameInviteRequestDto>(viewModel);
            var result = await _gameService.Invite(gameDto);

            return Ok(result);
        }

        [HttpPost("accept/{gameId}")]
        public async Task<ActionResult> AcceptInvitation(Guid gameId)
        {
            await _gameService.Accept(gameId);

            return Ok();
        }


    }
}