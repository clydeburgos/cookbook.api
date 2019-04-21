using System;
using System.Collections.Generic;

namespace CookBook.Data.Models
{
    public partial class Recipe
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }

        public string PhotoUrl { get; set; }
    }
}
