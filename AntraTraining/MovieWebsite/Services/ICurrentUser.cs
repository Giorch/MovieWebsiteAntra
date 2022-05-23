namespace MovieWebsite.Services
{
    public interface ICurrentUser
    {
        int? userId { get; }
        bool IsAdmin { get; }
        bool IsAuthenticated { get; }
        string Email { get; }
        string FullName { get; }
        IEnumerator<string> Roles { get; }
    }
}
