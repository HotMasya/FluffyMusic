using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;

namespace FluffyMusic.Web.OAuth.Discord
{
    public sealed class DiscordOAuthOptions : OAuthOptions
    {
        public DiscordOAuthOptions()
        {
            AuthorizationEndpoint = DiscordOAuthDefaults.BaseUrl;
            TokenEndpoint = DiscordOAuthDefaults.TokenUrl;
            UserInformationEndpoint = DiscordOAuthDefaults.MeUrl;
            Scope.Add(DiscordScopes.Identify);
            Scope.Add(DiscordScopes.Email);
            Scope.Add(DiscordScopes.Guilds);

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id", ClaimValueTypes.UInteger64);
            ClaimActions.MapJsonKey(ClaimTypes.Name, "username", ClaimValueTypes.String);
            ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
            ClaimActions.MapJsonKey(DiscordClaimTypes.Discriminator, "discriminator", ClaimValueTypes.UInteger32);
            ClaimActions.MapJsonKey(DiscordClaimTypes.Avatar, "avatar", ClaimValueTypes.String);
            ClaimActions.MapJsonKey(DiscordClaimTypes.Verified, "verified", ClaimValueTypes.Boolean);
        }
    }
}
