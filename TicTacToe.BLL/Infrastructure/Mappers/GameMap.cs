using AutoMapper;
using TicTacToe.BLL.Dto.Game;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Infrastructure.Mappers;

public class GameMap :  Profile
{
    public GameMap()
    {
        CreateMap<CellEvent, CellEventDto>();
    }
}