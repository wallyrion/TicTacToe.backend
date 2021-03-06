using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.API.ViewModels.Game;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.BLL.Services.Interfaces;

namespace TicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GameController : BaseApiController
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
        public async Task<ActionResult<GameInvitationDto>> InviteToGame([FromBody] GameInviteRequestVM requestVM)
        {
            var result = await _gameService.Invite(UserId, requestVM.OpponentId);

            return Ok(result);
        }

        [HttpPost("accept/{gameId}")]
        public async Task<ActionResult> AcceptInvitation(Guid gameId)
        {
            await _gameService.Accept(gameId);

            return Ok();
        }

        [HttpPost("next-turn")]
        public async Task<GameEventDto> HandleNextTurn(NextTurnRequestVM request)
        {
            var dto = _mapper.Map<NextTurnRequestDto>(request);
            dto.UserId = UserId;
            var gameEventResponse = await _gameService.HandleNextTurn(dto);

            return gameEventResponse;
        }
    }
}