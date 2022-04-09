using AutoMapper;
using TicTacToe.BLL.Dto.User;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Infrastructure.Mappers;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<User, UserDto>();
        CreateMap<LoginDto, User>();
    }
}