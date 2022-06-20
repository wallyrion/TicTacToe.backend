namespace TicTacToe.Common.Options;

public class TicTacToeDbOptions
{
    public static string SectionName = "TicTacToeDb";
    public string Endpoint { get; set; }
    public string AccountKey { get; set; }
    public string DatabaseName { get; set; }
}