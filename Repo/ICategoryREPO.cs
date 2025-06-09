using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Model;

namespace Repo
{
    public interface ICategoryREPO
    {
        List<Category> GetCategory();
    }
}
