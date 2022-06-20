using AutoMapper;
using TicTacToe.BLL.Dto.User;
using TicTacToe.DAL.Entities;

namespace TicTacToe.BLL.Infrastructure.Mappers;

public class UserMap : Profile
{
    public UserMap()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserSearchDto>()
            .ForMember(dist => dist.IsAlreadyInvited,
                opt => opt
                    .MapFrom((user, dto, _, context) =>
                    {
                        var currentUserId = (Guid)context.Items["CurrentUserId"];
                        return user.InvitedUsersIds.Contains(currentUserId.ToString());
                    }));
        CreateMap<LoginDto, User>();
    }
}