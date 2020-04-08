using GraphQL.Authorization;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.AspNetCore.Internal;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SneakerShop.API.Services
{
    public static class GraphQLAuthExtensions
    {
        public static void AddGraphQLAuth(this IServiceCollection services, Action<AuthorizationSettings> configure)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            services.AddTransient<IUserContextBuilder>(s => new UserContextBuilder<GraphQLUserContext>(context =>
            {
                var userContext = new GraphQLUserContext
                {
                    User = context.User
                };

                return Task.FromResult(userContext);
            }));

            services.AddSingleton(s =>
            {
                var authSettings = new AuthorizationSettings();
                configure(authSettings);
                return authSettings;
            });
        }
    }

    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }

    public class MapRolesForGraphQLMiddleware
    {
        private readonly RequestDelegate _next;

        public MapRolesForGraphQLMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJIZWx2ZXRpdXMuTmFneUBob3RtYWlsLmNvbSIsImp0aSI6IjRlNjc1ZjRmLTMwN2QtNGVmYi05MzM0LTIzOTdlYTk4ZjY1ZCIsIm15RXh0cmFLZXkiOiJteUV4dHJhVmFsdWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTU3ODkwMzcwOSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0In0.wr8wgxMQp4ZXptzpZnPAo_b7NKuwAFVJP3GtgvpuyC0";
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var metadata = context.User.Claims.SingleOrDefault(x => x.Type.Equals("metadata"));

            if (metadata != null)
            {
                var roleContainer = JsonConvert.DeserializeObject<RoleContainer>(metadata.Value);
                (context.User.Identity as ClaimsIdentity).AddClaim(new Claim("Role", string.Join(", ", roleContainer.Roles)));
            }

            await _next(context);
        }
    }

    public class RoleContainer
    {
        public String[] Roles { get; set; }
    }
}
