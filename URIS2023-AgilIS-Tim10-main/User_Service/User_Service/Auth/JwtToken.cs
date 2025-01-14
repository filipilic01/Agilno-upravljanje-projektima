﻿namespace User_Service.Auth
{
    public class JwtToken
    {
        public string Token { get; set; }
        public string ExpiresOn { get; set; }

        public string Username { get; set; }
        public string Role { get; set; }

        public Guid UserId { get; set; }
    }
}
