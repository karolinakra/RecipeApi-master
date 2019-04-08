using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Auth
{
    public class KeyAuthorizeAttribute : AuthorizeAttribute

    {
        private readonly string policyName;

        public KeyAuthorizeAttribute(PolicyEnum policy)

        {

            policyName = policy.ToString();

            Policy = policyName;

        }

    }
}
