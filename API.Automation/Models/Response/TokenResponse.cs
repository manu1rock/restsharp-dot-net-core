using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace API.Automation.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; }
        [JsonPropertyName("access_token")]
        public string AccessToken { get; }
    }
}
