using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeManager.Models
{
    public class UserIngredient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserIngredientID { get; set; }
        [ForeignKey("UserRecipe")]
        public int UserRecipeID { get; set; }
        [ForeignKey("Ingredient")]
        public int IngredientID { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual UserRecipe UserRecipe { get; set; }
    }
}