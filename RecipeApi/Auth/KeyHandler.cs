using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RecipeApi.Helpers;
using RecipeApi.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Auth
{
    public class KeyHandler : AuthorizationHandler<KeyRequirement>

    {

        private IHttpContextAccessor contextAccessor;

        private readonly IApiKeyRepo repo;



        public KeyHandler(IHttpContextAccessor contextAccessor, IApiKeyRepo repo)

        {

            this.contextAccessor = contextAccessor;

            this.repo = repo;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KeyRequirement requirement)

        {

            var roles = repo.GetDictionary();

            var httpContext = contextAccessor.HttpContext;

            string key = httpContext.Request.Headers[StaticValues.ApiKey];

            if (key != null)

            {

                string role = roles.ContainsKey(key) ? roles[key] : PolicyEnum.Lack.ToString();

                if (role == requirement.Role.ToString())

                {

                    context.Succeed(requirement);

                }

                else

                {

                    httpContext.Response.StatusCode = 401;

                    httpContext.Response.WriteAsync(StaticValues.AccesDenied);

                }

            }

            else

            {

                httpContext.Response.StatusCode = 400;

                httpContext.Response.WriteAsync(StaticValues.MissingApiKey);

            }

            return Task.CompletedTask;



        }

    }
}
