namespace TicTacToe.API.ViewModels.User;

public record TokenResponseVM(UserVM UserViewModel, string AccessToken, string RefreshToken);