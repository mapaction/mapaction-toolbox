using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapAction
{
    public class Product
    {
        public string product;
        public string classification;
        public List<string> layers;

        public Product(string product, string classification, List<string> layers)
        {
            this.product = product;
            this.classification = classification;
            this.layers = layers;
        }
    }
}
