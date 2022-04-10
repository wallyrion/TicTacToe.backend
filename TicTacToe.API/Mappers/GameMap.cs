using AutoMapper;
using TicTacToe.API.ViewModels.Game;
using TicTacToe.BLL.Dto;
using TicTacToe.BLL.Dto.Game;

namespace TicTacToe.API.Mappers;

public class GameMap : Profile
{
    public GameMap()
    {
        CreateMap<GameInviteRequestVM, GameInviteRequestDto>();
    }
}