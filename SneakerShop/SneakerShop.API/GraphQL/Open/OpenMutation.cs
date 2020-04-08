using GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SneakerShop.API.GraphQL.Types.Input;
using SneakerShop.API.Models;
using SneakerShop.Models;
using SneakerShop.Models.Repositories;
using SneakerShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Open
{
    public class OpenMutation : ObjectGraphType
    {
        public OpenMutation(
            ILogger<OpenMutation> logger, 
            UserManager<User> userMgr, 
            IConfiguration configuration, 
            IPasswordHasher<User> hasher)
        {
            FieldAsync<StringGraphType>(
                "login",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<LoginInputType>> { Name = "login" }),
                resolve: async context =>
                {
                    var login = context.GetArgument<Login>("login");
                    var jwtsvc = new JWTServices<User>(configuration, logger, userMgr, hasher);
                    var token = await jwtsvc.GenerateJwtToken(login);


                    return token;
                });
        }
    }
}
