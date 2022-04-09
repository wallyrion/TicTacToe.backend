using AutoMapper;
using TicTacToe.API.ViewModels;
using TicTacToe.API.ViewModels.User;
using TicTacToe.BLL.Dto;
using TicTacToe.BLL.Dto.User;

namespace TicTacToe.API.Mappers;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<LoginRequestVM, LoginDto>();
        CreateMap<UserDto, UserVM>();
        
        //CreateMap<User, UserVM>();

    }
}