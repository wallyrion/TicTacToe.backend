using System.Globalization;

namespace TicTacToe.BLL.Exceptions;

public class AppException : Exception
{
    public virtual bool IsWriteToLogger { get; } = true;
    public AppException(string message) : base(message) { }
}