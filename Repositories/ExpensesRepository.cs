using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private AppDbContext _dbContext;
        public ExpensesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Expenses> GetAll()
        {
            return _dbContext.Expenses.ToList();
        }
        public Expenses GetById(int id)
        {
            return _dbContext.Expenses.Find(id);
        }
        public bool DeleteById(int? id)
        {
            var e = _dbContext.Expenses.Find(id);
            _dbContext.Expenses.Remove(e);
            return _dbContext.SaveChanges() == 1 ? true : false;
        }
        public void Create(int cat_id, string name, decimal exp)
        {
            _dbContext.Expenses.Add(new Expenses() {ExpCategoryId = cat_id, Name = name, Cost= exp });
            _dbContext.SaveChanges();
        }
        public void Update(int updateableId, int cat_id, string updatename, decimal updateexp)
        {
            var e = _dbContext.Expenses.Find(updateableId);
            e.ExpCategoryId = cat_id;
            e.Name = updatename;
            e.Cost = updateexp;
            _dbContext.SaveChanges();
        }


        // Išlaidos pagal kategoriją
        public IEnumerable<Expenses> GetExpCat(int catId)
        {

            var isl = _dbContext.Expenses.AsNoTracking()
                     .Where(x => (x.ExpCategoryId == catId)).ToList();
            return isl;
        }

        // Išlaidų pagal kategorijas grafiko duomenys
      
    }
}
