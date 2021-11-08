using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Repositories
{
    public class ExpCategoryRepository : IExpCategoryRepository
    {
        private AppDbContext _dbContext;
        public ExpCategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ExpCategory> GetAll()
        {
            return _dbContext.ExpCategory.ToList();
        }
        public ExpCategory GetById(int id)
        {
            return _dbContext.ExpCategory.Find(id);
        }
        public bool DeleteById(int? id)
        {
            var e = _dbContext.ExpCategory.Find(id);
            _dbContext.ExpCategory.Remove(e);
            return _dbContext.SaveChanges() == 1 ? true : false;
        }
        public void Create(string category)
        {
            _dbContext.ExpCategory.Add(new ExpCategory() { Category= category});
            _dbContext.SaveChanges();
        }
        public void Update(int updateableId, string updatedCategoty)
        {
            var e = _dbContext.ExpCategory.Find(updateableId);
            e.Category = updatedCategoty;
            _dbContext.SaveChanges();
        }

    }
}
