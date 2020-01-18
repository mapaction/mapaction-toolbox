using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapActionToolbar_Core
{
    public class Product
    {
        public string mapNumber;
        public string product;
        public string category;
        public List<string> layers;

        public Product(string product, string category, string mapNumber, List<string> layers)
        {
            this.mapNumber = mapNumber;
            this.product = product;
            this.category = category;
            this.layers = layers;
        }
    }
}
