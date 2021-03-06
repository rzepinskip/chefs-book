using System;
using System.Collections.Generic;
using System.Linq;

namespace ChefsBook.Core.Models
{
    public class Recipe
    {
        private readonly List<Ingredient> ingredients = new List<Ingredient>();
        private readonly List<Step> steps = new List<Step>();
        private readonly List<RecipeTag> tags = new List<RecipeTag>();

        public Guid RecipeId { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public bool IsPrivate { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public int? Servings { get; private set; }
        public string Notes { get; private set; }
        public bool IsDeleted { get; private set; }
        public IReadOnlyList<Ingredient> Ingredients => ingredients;
        public IReadOnlyList<Step> Steps => steps;
        public IReadOnlyList<RecipeTag> Tags => tags;

        private Recipe() { }

        public static Recipe Create(
            Guid userId, string title, bool isPrivate, string description, string image, TimeSpan? duration, int? servings, string notes, 
            IList<Ingredient> ingredients, IList<Step> steps)
        {
            return Recipe.Create(Guid.NewGuid(), userId, title, isPrivate, description, image, duration, 
                servings, notes, ingredients, steps);
        }

        private static Recipe Create(
            Guid recipeId, Guid userId, string title, bool isPrivate, string description, string image, TimeSpan? duration, int? servings,
            string notes, IList<Ingredient> ingredients, IList<Step> steps)
        {
            if (userId == null)
            {
                throw new ArgumentException("Recipe's user id cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Recipe title cannot be empty or whitespace.");
            }

            var recipe = new Recipe
            {
                RecipeId = recipeId,
                UserId = userId,
                Title = title,
                IsPrivate = isPrivate,
                Description = description,
                Image = image,
                Duration = duration,
                Servings = servings,
                Notes = notes
            };

            if (ingredients != null)
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    recipe.ingredients.Add(Ingredient.Create(ingredients[i].Name, ingredients[i].Quantity, i));
                }
            }

            if (steps != null)
            {
                for (int i = 0; i < steps.Count; i++)
                {
                    recipe.steps.Add(Step.Create(steps[i].Duration, steps[i].Description, i));
                }
            }

            return recipe;
        }

        public void Update(
            string title, bool isPrivate, string description, string image, TimeSpan? duration, int? servings, string notes,
            IList<Ingredient> ingredients, IList<Step> steps)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty or whitespace.");
            }

            Title = title;
            IsPrivate = isPrivate;
            Description = description;
            Image = image;
            Duration = duration;
            Servings = servings;
            Notes = notes;
            
            this.ingredients.Clear();
            this.steps.Clear();

            if (ingredients != null)
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    this.ingredients.Add(Ingredient.Create(ingredients[i].Name, ingredients[i].Quantity, i));
                }
            }

            if (steps != null)
            {
                for (int i = 0; i < steps.Count; i++)
                {
                    this.steps.Add(Step.Create(steps[i].Duration, steps[i].Description, i));
                }
            }
        }

        public void AddTags(IList<RecipeTag> tags)
        {
            this.tags.AddRange(tags);
        }

        public void UpdateTags(IList<RecipeTag> tags)
        {
            this.tags.Clear();
            this.tags.AddRange(tags);
        }

        public void RemoveTags()
        {
            this.tags.Clear();
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}