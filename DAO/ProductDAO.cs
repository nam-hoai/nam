using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProduct()
        {
            string sql = @"
                select p.product_name, p.product_id, p.author, p.URL_image, p.date_of_insert, p.number_product, p.isAvailable,
		               c.cat_id, c.cat_name
                from Products p
                JOIN Category c 
                on p.cat_id = c.cat_id";
            return DbContext.Query(sql, reader => new Product
            {
                product_name=reader.GetString(0),
                product_id=reader.GetString(1),
                author=reader.GetString(2),
                URL_image=reader.GetString(3),
                date_of_insert = reader.GetDateTime(4).ToString("yyyy-MM-dd"),
                number_product =reader.GetInt32(5),
                isAvailable=reader.GetBoolean(6),
                category = new Category(
                    reader.GetString(7),
                    reader.GetString(8))
            });
        }
        public static Product? Search(Product p)
        public static int Add(Product p)
        public static int Delate(Product p)
        public static int Update(Product p)
    }
}
