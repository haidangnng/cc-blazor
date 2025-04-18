namespace QuestionLairClient.Models.Auth;

public class TokenStorage : ITokenStorage
{
    public string? Token { get; set; }
}
