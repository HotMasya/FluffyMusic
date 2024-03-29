﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluffyMusic.Web.OAuth.Discord
{
    public class DiscordOAuthHandler : OAuthHandler<DiscordOAuthOptions>
    {
        public DiscordOAuthHandler(IOptionsMonitor<DiscordOAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(
            ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var payload = JsonDocument.Parse(json).RootElement;
            var oauthContext = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload);          
            oauthContext.RunClaimActions();
            await Events.CreatingTicket(oauthContext);
            return new AuthenticationTicket(oauthContext.Principal, oauthContext.Properties, Scheme.Name);
        }
    }
}
