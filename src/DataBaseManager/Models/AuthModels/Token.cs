namespace DataBaseManager.Models.AuthModels;

public class Token
{
    public Guid Id { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public Guid UserSessionId { get; set; }
    public UserSession UserSession { get; set; } = default!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public Guid UserId { get; set; }
}
