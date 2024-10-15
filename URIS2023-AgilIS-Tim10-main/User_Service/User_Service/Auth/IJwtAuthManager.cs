namespace User_Service.Auth
{
    public interface IJwtAuthManager
    {
        JwtToken Authenticate(string UserName, string Password, string Role, Guid UserId);
    }
}
