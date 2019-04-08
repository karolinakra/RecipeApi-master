using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Auth
{
    public enum PolicyEnum
    {

            Admin = 1,
            User = 2,
            Reader = 4,
            Lack = 1000

    }
}
