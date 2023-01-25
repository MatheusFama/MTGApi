using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MTGApi.Entities;
using MTGApi.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MTGApi.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _appSettings = options.Value;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (token != null)
                await attachAccountToContext(context, dataContext, token);

            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, DataContext dataContext, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };
                tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;
                
                int accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var account = await dataContext.Accounts.FindAsync(accountId);

                context.Items["Account"] = account;

            }
            catch
            {

            }
        }
    }
}
