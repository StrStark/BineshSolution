namespace BineshSoloution.Models.AuthModels;

public class UserSession
{
    public Guid SessionUniqueId { get; set; }

    public string? IP { get; set; }

    public string? Device { get; set; }

    public string? Address { get; set; }

    public DateTimeOffset StartedOn { get; set; }

    public DateTimeOffset? RenewedOn { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Token Token { get; set; } = default!;
    public TimeSpan SessionLifetime => TimeSpan.FromDays(7); // we will set titfrom appsetting.json later
    public bool IsExpired() => DateTimeOffset.UtcNow - RenewedOn >= SessionLifetime;
}
