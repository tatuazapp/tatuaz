using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using Tatuaz.Gateway.Configuration.Options;

namespace Tatuaz.Gateway.Configuration.Helpers;

public static class Auth0Helpers
{
    public static Auth0Options GetAuth0Options(this IConfiguration configuration)
    {
        var options = new Auth0Options();
        configuration.GetSection("Auth0").Bind(options);
        return options;
    }

    public static string GetAuth0Token(Auth0Options auth0Options)
    {
        var client = new RestClient($"https://{auth0Options.Domain}/oauth/token");
        var request = new RestRequest { Method = Method.Post };
        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json",
            $"{{\"client_id\":\"{auth0Options.ClientId}\",\"client_secret\":\"{auth0Options.ClientSecret}\",\"audience\":\"{auth0Options.Audience}\",\"grant_type\":\"client_credentials\"}}",
            ParameterType.RequestBody);

        var response = client.Execute(request);
        if (!response.IsSuccessful || response.Content == null)
        {
            throw new Exception("Auth0 token request failed");
        }

        var auth0Token = JsonConvert.DeserializeAnonymousType(response.Content, new { accessToken = "" });
        if (auth0Token == null)
        {
            throw new Exception("Auth0 token request failed");
        }

        return auth0Token.accessToken;
    }
}
