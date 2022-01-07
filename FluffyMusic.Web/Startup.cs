using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Victoria;
using Discord.WebSocket;
using Discord.Commands;
using FluffyMusic.Core.Options;
using FluffyMusic.Core;
using FluffyMusic.Web.OAuth.Discord;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FluffyMusic.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //LavaConfig lavaCfg = new LavaConfig();
            //Configuration.GetSection("Lavalink").Bind(lavaCfg);
            //services.Configure<AudioOptions>(Configuration.GetSection("Audio"));
            //services.Configure<BotOptions>(Configuration.GetSection("Bot"));
            //services.AddSingleton(lavaCfg);
            //services.AddSingleton<DiscordSocketClient>();
            //services.AddSingleton<CommandService>();
            //services.AddSingleton<LavaNode>();
            //services.AddSingleton<FluffyClient>();
            //services.AddSingleton<CommandHandler>();
            //services.AddSingleton<AudioService>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    { 
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtValidation:Issuer"],
                        ValidAudience = Configuration["JwtValidation:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtValidation:Secret"]))
                    };
                })
                .AddDiscord(o =>
                {
                    o.ClientId = Configuration["DiscordOAuth:ClientId"];
                    o.ClientSecret = Configuration["DiscordOAuth:ClientSecret"];
                    o.AccessDeniedPath = new PathString(Configuration["DiscordOAuth:AccessDeniedPath"]);
                    o.CallbackPath = new PathString("/signin-discord");
                });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
