using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace MapAction
{
    public class Cookbook
    {
        public List<Product> recipes;

        public HashSet<string> classifications;

        public Cookbook(string filePath)
        {
            recipes = new List<Product>();
            classifications = new HashSet<string>();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var cookbook = JsonConvert.DeserializeObject<Cookbook>(json);

                foreach (var r in cookbook.recipes)
                {
                    //Console.WriteLine("{0} {1)\n", r.classification, r.product);
                    recipes.Add(new Product(r.product, r.classification, r.layers));
                    classifications.Add(r.classification);
                }
            }
        }

        public List<Product> recipeByClassification(string classification)
        {
            List<Product> result = new List<Product>();

            foreach (var recipe in recipes)
            {
                if (recipe.classification == classification)
                {
                    result.Add(recipe);
                }
            }
            return result;
        }
    }
}
