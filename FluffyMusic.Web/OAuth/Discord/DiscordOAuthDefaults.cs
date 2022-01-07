namespace FluffyMusic.Web.OAuth.Discord
{
    public sealed class DiscordOAuthDefaults
    {
        public const string AuthenticationScheme = "Discord";
        public const string DisplayName = "Discord";

        public const string BaseUrl = "https://discord.com/api/oauth2/authorize";
        public const string TokenUrl = "https://discord.com/api/oauth2/token";
        public const string RevokeUrl = "https://discord.com/api/oauth2/token/revoke";
        public const string MeUrl = "https://discordapp.com/api/users/@me";

        public const string DiscordImgUrl = "https://cdn.discordapp.com/";
    }
}
