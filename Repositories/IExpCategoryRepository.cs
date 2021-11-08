using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Repositories
{
    public interface IExpCategoryRepository
    {
        IEnumerable<ExpCategory> GetAll();
        ExpCategory GetById(int id);
        bool DeleteById(int? id);
        void Create(string name);
        void Update(int updateableId, string updatedCategory);
    }
}
