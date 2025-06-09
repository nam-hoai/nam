using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repo;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryREPO iCategoryREPO;
        public CategoryService()
        {
            iCategoryREPO = new CategoryREPO();
        }
        public List<Category> GetCategory()
        {
            return iCategoryREPO.GetCategory();
        }
    }
}
