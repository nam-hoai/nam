using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DAO
{
    public class ProductDAO
    {
        private readonly DBContext _context;
        // Delegates để thao tác CRUD
        private DBContext.AddEntity<Product> _addP;
        private DBContext.RemoveEntity<Product> _removeP;
        private DBContext.FindEntity<Product> _searchP;
        private DBContext.GetAllEntity<Product> _getAllProduct;
        public ProductDAO()
        {
            _context = DBContext.Instance;
            //assign delegate
            _addP = _context.Add<Product>();
            _removeP = _context.Delete<Product>();
            _searchP = _context.Search<Product>();
            _getAllProduct = _context.GetAll<Product>();
        }
        public List<Product> GetProduct()
        {
            return _getAllProduct(p=>p.category);
        }
        //public static Product? Search(Product p);
        //public static int Add(Product p);
        //public static int Delate(Product p);
        //public static int Update(Product p);
    }
}
