﻿using System;

namespace MyRestaurant.Services.Account
{
    public class JWTSettings
    {
        public string AccessTokenSecret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenExpirationInMinutes { get; set; }
        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpirationInMinutes { get; set; }

    }
    
}
