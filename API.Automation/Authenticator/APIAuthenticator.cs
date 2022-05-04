using API.Automation.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation.Authenticator
{
    public class APIAuthenticator : AuthenticatorBase
    {
        readonly string _baseUrl;
        readonly string _clientId;
        readonly string _clientSecret;
        public APIAuthenticator(string baseUrl, string clientId, string clientSecret) : base("")
        {
            _baseUrl = baseUrl;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public APIAuthenticator() : base("")
        {
            Token = "Bearer ebywews1234eds"; //ConfigurationManager.AppSettings["TOKEN"];
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);            
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl);
            using var client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
            };

            var request = new RestRequest("oauth2/token")
                .AddParameter("grant_type", "client_credentials");
            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response!.TokenType} {response!.AccessToken}";
        }
    }
}
