using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategory()
        {
            string sql = "select * from Category";
            return DbContext.Query(sql, reader => new Category
            {
                cat_id=reader.GetString(1),
                cat_name=reader.GetString(2)
            });
        }
    }
}
