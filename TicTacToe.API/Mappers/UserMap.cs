using AutoMapper;
using TicTacToe.API.Models;
using TicTacToe.API.ViewModels;

namespace TicTacToe.API.Mappers;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<LoginViewModel, User>()
            .ForMember(x => x.Id, 
                opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<User, UserViewModel>();

    }
}