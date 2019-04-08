using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecipeApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Models
{
    public class RecipeContext : DbContext
    {
     
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }
        public RecipeContext()
        {
            //this.configuration = configuration;
            //this.connectionName = connectionName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(StaticValues.ConnectionHelper);

        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Key> Keys { get; set; }
    }
}
