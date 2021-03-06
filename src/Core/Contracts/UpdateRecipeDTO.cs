using System;
using System.Collections.Generic;

namespace ChefsBook.Core.Contracts
{
    public class UpdateRecipeDTO
    {
        public bool IsPrivate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? Servings { get; set; }
        public string Notes { get; set; }
        public List<UpdateRecipeIngredientDTO> Ingredients { get; set; }
        public List<UpdateRecipeStepDTO> Steps { get; set; }
        public List<UpdateRecipeTagDTO> Tags { get; set; }
    }

    public class UpdateRecipeIngredientDTO
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

    public class UpdateRecipeStepDTO
    {
        public TimeSpan? Duration { get; set; }
        public string Description { get; set; }
    }

    public class UpdateRecipeTagDTO
    {
        public string Name { get; set; }
    }
}