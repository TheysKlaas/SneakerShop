using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SneakerShop.API.GraphQL;
using SneakerShop.API.GraphQL.Admin;
using SneakerShop.API.GraphQL.NoSQL;
using SneakerShop.API.GraphQL.Open;
using SneakerShop.API.GraphQL.Restricted;
using SneakerShop.API.Models;

namespace SneakerShop.API.Controllers
{
    [Route("/graphql")]
    [ApiController]
    public class GraphqlController : ControllerBase
    {
        private readonly IDependencyResolver resolver;
        private readonly DataLoaderDocumentListener dataLoaderListener;

        public GraphqlController(IDependencyResolver resolver, DataLoaderDocumentListener dataLoaderListener)
        {
            this.resolver = resolver;
            this.dataLoaderListener = dataLoaderListener;
        }
        [HttpPost]
        public async Task<ActionResult> OpenPost([FromBody] GraphQLQuery query)
        {
            var schema = new OpenSchema(resolver);
            var inputs = query.Variables.ToInputs();
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.Listeners.Add(dataLoaderListener);
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpPost("restricted")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, User")]
        public async Task<ActionResult> RestrictedPost([FromBody] GraphQLQuery query)
        {
            var schema = new RestrictedSchema(resolver);
            var inputs = query.Variables.ToInputs();
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.Listeners.Add(dataLoaderListener);
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }        
        [HttpPost("admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AdminPost([FromBody] GraphQLQuery query)
        {
            var schema = new AdminSchema(resolver);
            var inputs = query.Variables.ToInputs();
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.Listeners.Add(dataLoaderListener);
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        [HttpPost("admin/nosql")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> nosqmPost([FromBody] GraphQLQuery query)
        {
            var schema = new NoSQLSchema(resolver);
            var inputs = query.Variables.ToInputs();
            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.Listeners.Add(dataLoaderListener);
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}