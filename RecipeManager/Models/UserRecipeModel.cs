using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeManager.Models
{
    public class UserRecipeModel
    {
        
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
}
    public class RecipeIngredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public bool isChecked { get; set; }
    }
}