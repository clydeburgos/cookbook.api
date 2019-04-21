using CookBook.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> Get(Guid id);
        Task<IEnumerable<Recipe>> GetByUser(string id);
        Task Save(Recipe recipe);
        Task Delete(Guid id);

        Task<string> GetRecipeImage(string recipe);
    }
}
