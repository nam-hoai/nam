using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Category
    {
        public Category() { 
            Products = new HashSet<Product>();
        }
        public Category(string id, string name)
        {
            this.cat_id=id;
            this.cat_name= name;
        }
        public string cat_id { get; set; }
        public string cat_name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
