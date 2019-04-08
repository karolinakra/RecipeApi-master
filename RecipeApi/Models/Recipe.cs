using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Models
{
    public class Recipe
    {
        //public Recipe()
        //{
        //    Categories = new HashSet<Category>();
        //}
        public int ID { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        //[MaxLength(100)]
        //public string Category { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Bladzik")]
        public string Body { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
