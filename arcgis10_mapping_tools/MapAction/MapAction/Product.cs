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
        public List<ProductLayer> layers;
        public string summary;

        public Product(string product, 
                       string category, 
                       string mapNumber, 
                       List<ProductLayer> layers, 
                       string summary)
        {
            this.mapNumber = mapNumber;
            this.product = product;
            this.category = category;
            this.layers = layers;
            this.summary = summary;
        }
    }
}
