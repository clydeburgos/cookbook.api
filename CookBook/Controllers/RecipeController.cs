using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CookBook.API.Extensions;
using CookBook.Data.Models;
using CookBook.Model;
using CookBook.Service;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            string id = this.GetUserId();
            var data = await this.recipeService.GetAll();
            if (!string.IsNullOrEmpty(id)) {
                data = data.Where(r => r.UserId == id);
            }
            if (data.Count() > 0) {
                var dataModel = this.mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeViewModel>>(data);
                return Ok(dataModel);
            }
            
            return Ok(new List<RecipeViewModel>());
        }

        [Route("api/recipe")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromBody]RecipeViewModel model) {
            var data = this.mapper.Map<RecipeViewModel, Recipe>(model);
            await this.recipeService.Save(data);
            return Ok(true);
        }

        [Route("api/recipe/{id}")]
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.recipeService.Delete(id);
            return Ok(true);
        }
    }
}