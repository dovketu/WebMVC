using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Repositories;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class ExpCategoryController : Controller
    {
        private IExpCategoryRepository _expCatRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        public ExpCategoryController(IExpCategoryRepository expCatRepo, ILogger<ExpCategoryController> logger,IMapper mapper)

        {
            _expCatRepo = expCatRepo;
            _logger = logger;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            return View();
        }

  
        public ActionResult ExpCategoryList()
        {
            IEnumerable<ExpCategoryListView> cat_list = null;
            IEnumerable<ExpCategory> categories;
            categories = _expCatRepo.GetAll();
            if (categories != null)
            {
                cat_list = _mapper.Map<IEnumerable<ExpCategory>, IEnumerable<ExpCategoryListView>>(categories);
                return View(cat_list);
            }
            return null;
        }

        [HttpGet]
        public ActionResult CreateExpCategory()
        {
            CreatEditExpCategoryVM model = new CreatEditExpCategoryVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExpCategory(CreatEditExpCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var cat = _mapper.Map<CreatEditExpCategoryVM, ExpCategory>(model);
                _expCatRepo.Create(cat.Category);
                return RedirectToAction("ExpCategoryList");
            }
            return View(model);
        }


        public ActionResult DeleteCategory(int? id)
        {       
            return (id == null || !_expCatRepo.DeleteById(id))
               ? NotFound()
               : RedirectToAction("ExpCategoryList");
        }

        [HttpGet]
        public ActionResult EditExpCat(int id)
        {
            var selectedCat = _expCatRepo.GetById(id);
            var selectedViewModel = _mapper.Map<ExpCategory, CreatEditExpCategoryVM>(selectedCat);
            return View(selectedViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExpCat(CreatEditExpCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var cat = _mapper.Map<CreatEditExpCategoryVM, ExpCategory>(model);
                _expCatRepo.Update(cat.Id, cat.Category);
              return RedirectToAction("ExpCategoryList");
            }
            return View(model);
        }

    }
}

