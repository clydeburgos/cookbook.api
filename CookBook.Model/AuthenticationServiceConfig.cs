using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Model
{
    public class AuthenticationServiceConfig
    {
        public string JwtSigningKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public long JwtValidForMinutes { get; set; }
    }
}
