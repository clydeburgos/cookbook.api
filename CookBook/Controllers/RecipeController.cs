using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookBook.Data.Models;
using CookBook.Model;
using CookBook.Service;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IMapper mapper;

        public RecipeController(IRecipeService recipeService, IMapper mapper) {
            this.recipeService = recipeService;
            this.mapper = mapper;
        }

        [Route("api/recipe")]
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.recipeService.GetAll();
            if (data.Count() > 0) {
                var dataModel = this.mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeViewModel>>(data);
                return Ok(dataModel);
            }
            
            return Ok(new List<RecipeViewModel>());
        }

        [Route("api/recipe")]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]RecipeViewModel model) {
            var data = this.mapper.Map<RecipeViewModel, Recipe>(model);
            await this.recipeService.Save(data);
            return Ok(true);
        }

        [Route("api/recipe/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.recipeService.Delete(id);
            return Ok(true);
        }
    }
}