namespace TicTacToe.BLL.Exceptions
{
    public class EntityNotFoundException : AppException
    {
        public override bool IsWriteToLogger { get; } = false;

        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}