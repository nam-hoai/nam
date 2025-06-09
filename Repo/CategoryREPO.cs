using DAO;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class CategoryREPO : ICategoryREPO
    {
        public List<Category> GetCategory() => CategoryDAO.GetCategory();
    }
}
