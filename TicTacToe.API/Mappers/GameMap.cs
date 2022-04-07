using AutoMapper;
using TicTacToe.API.Dto;
using TicTacToe.API.Models;
using TicTacToe.API.ViewModels;
using TicTacToe.API.ViewModels.Game;

namespace TicTacToe.API.Mappers;

public class GameMap : Profile
{
    public GameMap()
    {
        CreateMap<GameInviteViewModel, GameInviteDto>();
        CreateMap<GameInviteDto, Game>()
            .ForMember(x => x.Id, 
                opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}