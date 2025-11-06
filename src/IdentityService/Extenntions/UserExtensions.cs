using DataBaseManager.Models;

namespace DataBaseManager.Extenntions;

public static class UserExtensions
{
    public static UserSession? GetValidSession(this User user)
    {
        return user.Sessions.FirstOrDefault(s => !s.IsExpired());
    }

    public static void CleanupExpiredSessions(this User user)
    {
        user.Sessions.RemoveAll(s => s.IsExpired());
    }
}