using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToe.API.Dto;
using TicTacToe.API.Models;
using TicTacToe.API.SignalR;
using TicTacToe.API.ViewModels;
using TicTacToe.API.ViewModels.Game;

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
        public async Task<ActionResult<GameInvitation>> InviteToGame(GameInviteViewModel viewModel)
        {
            var gameDto = _mapper.Map<GameInviteDto>(viewModel);
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