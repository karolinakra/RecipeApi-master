using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Models
{
    public class Key
    {
        [Key]
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
