using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RecipeApi.Auth
{
    public class KeyRequirement : IAuthorizationRequirement
    {
        public PolicyEnum Role { get; private set; }

        public KeyRequirement(PolicyEnum Role)

        {
            this.Role = Role;

        }
    }
}
