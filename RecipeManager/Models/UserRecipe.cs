using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecipeManager.Models
{
    public class UserRecipe
    {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int UserRecipeID { get; set; }
            [ForeignKey("User")]
            public string UserID { get; set; }
            [ForeignKey("Recipe")]
            public int RecipeID { get; set; }
            public virtual ApplicationUser User { get; set; }
            public virtual Recipe Recipe { get; set; }
            
    }
}