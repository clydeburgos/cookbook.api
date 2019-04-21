using AutoMapper;
using CookBook.Data.Models;
using CookBook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.API.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Recipe, RecipeViewModel>().ReverseMap();
        }
    }
}
