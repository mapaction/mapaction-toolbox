using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MapActionToolbar_Core
{
    public class Cookbook
    {
        public List<Product> recipes;

        public HashSet<string> categories;

        public Cookbook(string filePath)
        {
            recipes = new List<Product>();
            categories = new HashSet<string>();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var cookbook = JsonConvert.DeserializeObject<Cookbook>(json);

                foreach (var r in cookbook.recipes)
                {
                    recipes.Add(new Product(r.product, r.category, r.mapNumber, r.layers));
                    categories.Add(r.category);
                }
            }
        }

        public List<Product> recipeByCategory(string category)
        {
            List<Product> result = new List<Product>();

            foreach (var recipe in recipes)
            {
                if (recipe.category == category)
                {
                    result.Add(recipe);
                }
            }
            return result;
        }
    }
}
