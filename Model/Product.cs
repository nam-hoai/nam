using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Product
    {
        public Product() {
            Orders = new HashSet<Order>();
            Carts = new HashSet<Cart>();
        }
        public Product(string name, string id, string author, string url, 
            string date, int quantity,bool check, Category c)
        {
            this.product_name= name;
            this.product_id= id;
            this.author= author;
            this.URL_image= url;
            this.date_of_insert= date;
            this.number_product= quantity;
            this.isAvailable= check;
            this.category= c;
           
        }
        public string product_name { get; set; }
        public string product_id { get; set; }
        public string author { get; set; }
        public string URL_image { get; set; }
        public string date_of_insert { get; set; }
        public int number_product { get; set; }
        public bool isAvailable { get; set; }
        public virtual Category category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
