using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace HermesSpa.DevelopmentAuthentication
{
    public class DevelopmentAuthenticationDefaults
    {
        public const string AuthenticationScheme = "Basic";
        public const string AuthenticationRealm = "DEVELOPMENT";
    }


    public class DevelopmentAuthenticationEvents
    {
        public Func<AuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; } = context => Task.CompletedTask;

        public Func<ValidateCredentialsContext, Task> OnValidateCredentials { get; set; } = context => Task.CompletedTask;

        public virtual Task AuthenticationFailed(AuthenticationFailedContext context) => OnAuthenticationFailed(context);

        public virtual Task ValidateCredentials(ValidateCredentialsContext context) => OnValidateCredentials(context);
    }


    public class ValidateCredentialsContext : ResultContext<DevelopmentAuthenticationOptions>
    {
        public ValidateCredentialsContext(HttpContext context, AuthenticationScheme scheme, DevelopmentAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class AuthenticationFailedContext : ResultContext<DevelopmentAuthenticationOptions>
    {
        public AuthenticationFailedContext(HttpContext context, AuthenticationScheme scheme, DevelopmentAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public Exception Exception { get; set; }
    }


    public class DevelopmentAuthenticationOptions : AuthenticationSchemeOptions
    {
        private string _realm;

        public DevelopmentAuthenticationOptions()
        {
        }

        public string Realm
        {
            get
            {
                return _realm;
            }

            set
            {
                if (!string.IsNullOrEmpty(value) && !IsAscii(value))
                {
                    throw new ArgumentOutOfRangeException("Realm", "Realm must be US ASCII");
                }

                _realm = value;
            }
        }

        public bool AllowInsecureProtocol { get; set; } = true;

        public new DevelopmentAuthenticationEvents Events { get; set; } = new DevelopmentAuthenticationEvents();


        private bool IsAscii(string input)
        {
            foreach (char c in input)
            {
                if (c < 32 || c >= 127)
                {
                    return false;
                }
            }

            return true;
        }
    }


    internal class DevelopmentAuthenticationHandler : AuthenticationHandler<DevelopmentAuthenticationOptions>
    {
        private const string _Scheme = "Basic";

        public DevelopmentAuthenticationHandler(
            IOptionsMonitor<DevelopmentAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected new DevelopmentAuthenticationEvents Events
        {
            get { return (DevelopmentAuthenticationEvents)base.Events; }
            set { base.Events = value; }
        }

        protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new DevelopmentAuthenticationEvents());

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith(_Scheme + ' ', StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.NoResult();
            }

            string encodedCredentials = authorizationHeader.Substring(_Scheme.Length).Trim();

            if (string.IsNullOrEmpty(encodedCredentials))
            {
                const string noCredentialsMessage = "No credentials";
                Logger.LogInformation(noCredentialsMessage);
                return AuthenticateResult.Fail(noCredentialsMessage);
            }

            try
            {
                string decodedCredentials = string.Empty;
                try
                {
                    decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to decode credentials : {encodedCredentials}", ex);
                }

                var delimiterIndex = decodedCredentials.IndexOf(':');
                if (delimiterIndex == -1)
                {
                    const string missingDelimiterMessage = "Invalid credentials, missing delimiter.";
                    Logger.LogInformation(missingDelimiterMessage);
                    return AuthenticateResult.Fail(missingDelimiterMessage);
                }

                var username = decodedCredentials.Substring(0, delimiterIndex);
                var password = decodedCredentials.Substring(delimiterIndex + 1);

                var validateCredentialsContext = new ValidateCredentialsContext(Context, Scheme, Options)
                {
                    Username = username,
                    Password = password
                };

                await Options.Events.ValidateCredentials(validateCredentialsContext);

                if (validateCredentialsContext.Result != null)
                {
                    var ticket = new AuthenticationTicket(validateCredentialsContext.Principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                var authenticationFailedContext = new AuthenticationFailedContext(Context, Scheme, Options)
                {
                    Exception = ex
                };

                await Options.Events.AuthenticationFailed(authenticationFailedContext);

                if (authenticationFailedContext.Result.Succeeded)
                {
                    return AuthenticateResult.Success(authenticationFailedContext.Result.Ticket);
                }

                if (authenticationFailedContext.Result.None)
                {
                    return AuthenticateResult.NoResult();
                }

                throw;
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            if (!Request.IsHttps && !Options.AllowInsecureProtocol)
            {
                const string insecureProtocolMessage = "Request is HTTP, Basic Authentication will not respond.";
                Logger.LogInformation(insecureProtocolMessage);
                Response.StatusCode = 500;
                var encodedResponseText = Encoding.UTF8.GetBytes(insecureProtocolMessage);
                Response.Body.Write(encodedResponseText, 0, encodedResponseText.Length);
            }
            else
            {
                Response.StatusCode = 401;

                var headerValue = _Scheme + $" realm=\"{Options.Realm}\"";
                Response.Headers.Append(HeaderNames.WWWAuthenticate, headerValue);
            }

            return Task.CompletedTask;
        }
    }
}


namespace Microsoft.Extensions.DependencyInjection
{
    using System.Security.Claims;
    using HermesSpa.DevelopmentAuthentication;

    public static class BasicAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder)
            => builder.AddBasic(DevelopmentAuthenticationDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddBasic(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<DevelopmentAuthenticationOptions> configureOptions)
            => builder.AddBasic(DevelopmentAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, Action<DevelopmentAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<DevelopmentAuthenticationOptions, DevelopmentAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }

    public static class DevelopmentAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddDevelopment(this AuthenticationBuilder app,
            params DevelopmentUser[] users)
        {
            return app.AddBasic(
                config => {
                    config.Realm = DevelopmentAuthenticationDefaults.AuthenticationRealm;
                    config.Events = new DevelopmentAuthenticationEvents
                    {
                        OnValidateCredentials = context =>
                        {
                            foreach (var user in users)
                            {
                                if (context.Username == user.Username && context.Password == user.Password)
                                {
                                    var claims = new[]
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, user.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                                        new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                    };

                                    var roleClaims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x, ClaimValueTypes.String, context.Options.ClaimsIssuer));
                                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims.Union(roleClaims), context.Scheme.Name));
                                    context.Success();
                                }
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
            );
        }
    }

    public class DevelopmentUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string[] Roles { get; set; }
    }
}
