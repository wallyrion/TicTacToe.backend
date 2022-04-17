namespace TicTacToe.BLL.Exceptions;

public class InvalidRequestException : AppException
{
    public InvalidRequestException(string message) : base(message) { }
}