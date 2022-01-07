using Microsoft.AspNetCore.Authentication;
using System;
using FluffyMusic.Web.OAuth.Discord;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DiscordOAuthExtensions
    {
        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder)
            => builder.AddDiscord(DiscordOAuthDefaults.AuthenticationScheme, options => { });

        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder, Action<DiscordOAuthOptions> options)
            => builder.AddDiscord(DiscordOAuthDefaults.AuthenticationScheme, DiscordOAuthDefaults.DisplayName, options);

        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder, string authenticationScheme, Action<DiscordOAuthOptions> options)
            => builder.AddDiscord(authenticationScheme, DiscordOAuthDefaults.DisplayName, options);

        public static AuthenticationBuilder AddDiscord(this AuthenticationBuilder builder,
            string authenticationScheme, string displayName, Action<DiscordOAuthOptions> options)
            => builder.AddOAuth<DiscordOAuthOptions, DiscordOAuthHandler>(authenticationScheme, displayName, options);
    }
}
