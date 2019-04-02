using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly RecipeContext db;

        public CategoryController(RecipeContext db)

        {

            this.db = db;
        }

        [HttpGet]
        public IActionResult GetCategory()
        {
            try
            {
                //throw new Exception("ojoj");
                return Ok(db.Categories.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetCategories(int id)
        {
            //var item = new Recipe
            //{
            //    ID = 1,
            //    Body = "OKi"
            //};
            //return Ok(item);

            try
            {
                var category = db.Categories.SingleOrDefault(a => a.ID == id);
                if (category == null)
                {
                    return NotFound($"Nie znaleziono id:{id}");
                }
                //throw new Exception("ojoj");
                return Ok(category);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }




        }
        [HttpPost]

        public IActionResult AddCategories([FromBody]Category category)
        {
            //return CreatedAtAction(nameof(GetRecipes), new { id = recipe.ID }, recipe);

            try
            {
                //TODO add validation
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return CreatedAtAction(nameof(GetCategories), new { id = category.ID }, category);
                }
                return BadRequest("Blad");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {


            try
            {
                var item = db.Categories.Find(id);
                item = category;
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
        }
    }
}