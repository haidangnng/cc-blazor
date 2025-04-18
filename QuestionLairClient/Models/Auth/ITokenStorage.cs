namespace QuestionLairClient.Models.Auth;

public interface ITokenStorage
{
    string? Token { get; set; }
}
