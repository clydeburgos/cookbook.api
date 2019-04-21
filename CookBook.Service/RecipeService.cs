using CookBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CookBook.Service.AzureServices;

namespace CookBook.Service
{
    public class RecipeService : IRecipeService
    {
        private CookbookContext dbContext;
        private BingSearchImageService bingSearchService;
        public RecipeService(CookbookContext dbContext, BingSearchImageService bingSearchService) {
            this.dbContext = dbContext;
            this.bingSearchService = bingSearchService;
        }

        public async Task<IEnumerable<Recipe>> GetAll() {
            return await this.dbContext.Recipe.ToListAsync();
        }

        public async Task<Recipe> Get(Guid id)
        {
            return await this.dbContext.Recipe.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Recipe>> GetByUser(string userId)
        {
            return await this.dbContext.Recipe.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task Save(Recipe recipe) {
            var recipeData = await Get(recipe.Id);
            if (recipeData == null) {
                if (string.IsNullOrEmpty(recipe.PhotoUrl)) {
                    recipe.PhotoUrl = await GetRecipeImage(recipe.Name);
                }
                recipe.Id = Guid.NewGuid();
                this.dbContext.Add(recipe);
            }
            else {
                recipeData.Name = recipe.Name;
                recipeData.Ingredients = recipe.Ingredients;
                recipeData.Instructions = recipe.Instructions;
                recipeData.PhotoUrl = recipe.PhotoUrl;
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id) {
            var recipeData = await Get(id);
            if (recipeData != null)
            {
                this.dbContext.Recipe.Remove(recipeData);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<string> GetRecipeImage(string recipe) {
            dynamic recipeSearch = await this.bingSearchService.SearchImage(recipe);
            var recipeFirstResult = Newtonsoft.Json.JsonConvert.DeserializeObject(recipeSearch.jsonResult)["value"][0];
            string photoUrl = recipeFirstResult["contentUrl"];
            return string.IsNullOrEmpty(photoUrl) ? "/assets/images/no-meat.png" : photoUrl;
        }
    }
}
