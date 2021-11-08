using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Repositories
{
    public interface IExpensesRepository
    {
        IEnumerable<Expenses> GetAll();
        Expenses GetById(int id);
        bool DeleteById(int? id);
        void Create(int cat_id, string name,  decimal exp);
        void Update(int updateableId, int cat_id, string updatedName, decimal updateExp);
        IEnumerable<Expenses> GetExpCat(int catId);
    }
}
