using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Models
{
    public class Category
    {

        public Category()
        {
            Recepies = new HashSet<Recipe>();
        }

        public int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recepies { get; set; }

    }
}
