using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Auth;
using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

        private readonly RecipeContext db;
        public RecipesController(RecipeContext db)

        {

            this.db = db;
        }

        [HttpGet]
        [KeyAuthorize(PolicyEnum.Reader)]
        public IActionResult GetRecipes()
        {
            try
            {
                //throw new Exception("ojoj");
                return Ok(db.Recipes.Include(a => a.Category).ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            
        }
        [HttpGet("{id}")]
        public IActionResult GetRecipes(int id)
        {
            //var item = new Recipe
            //{
            //    ID = 1,
            //    Body = "OKi"
            //};
            //return Ok(item);

            try
            {
                var recipe = db.Recipes.Include(a => a.Category).SingleOrDefault(a => a.ID == id);
                if (recipe == null)
                {
                    return NotFound($"Nie znaleziono id:{id}");
                }
                //throw new Exception("ojoj");
                return Ok(recipe);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }




        }
        [HttpPost]
        [KeyAuthorize(PolicyEnum.User)]
        public IActionResult AddRecipes([FromBody]Recipe recipe)
        {
            //return CreatedAtAction(nameof(GetRecipes), new { id = recipe.ID }, recipe);

            try
            {
                //TODO add validation
                if (ModelState.IsValid && CategoryExists(recipe.CategoryId))
                {
                    db.Recipes.Add(recipe);
                    db.SaveChanges();
                    return CreatedAtAction(nameof(GetRecipes), new { id = recipe.ID }, recipe);
                }
                return BadRequest("Blad");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }



        }
        [HttpPut("{id}")]
        public IActionResult UpdateRecipes(int id, Recipe recipe)
        {
          

            try
            {
                var item = db.Recipes.Find(id);
                item = recipe;
                if (id < 0)
                {
                    return NotFound();
                }
                db.Update(item);
                db.SaveChanges();
                return Ok(item);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


            //if (id < 0)
            //{
            //    return BadRequest();
            //}
            //return NoContent();

        }
        [HttpDelete("{id}")]
        [KeyAuthorize(PolicyEnum.Admin)]
        public ActionResult<Recipe> DeleteRecepies(int id)

        {
            try
            {
                var item = db.Recipes.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                db.Recipes.Remove(item);
                db.SaveChanges();
                return Ok(item);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        private bool CategoryExists(int? id)
        { 
            if(id == null)
            return true;
            return db.Categories.Any(a => a.ID == id);

        }
    }
}
