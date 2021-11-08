using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Repositories;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class ExpensesController : Controller
    {
   
        private IExpCategoryRepository _expCatRepo;
        private IExpensesRepository _expRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


            public ExpensesController(IExpCategoryRepository expCatRepo, IExpensesRepository expRepo, ILogger<ExpensesController> logger, IMapper mapper)

            {
                _expRepo = expRepo;
                _expCatRepo = expCatRepo;
                _logger = logger;
                _mapper = mapper;

            }
            public IActionResult Index()
            {
                return View();
            }


            public ActionResult ExpList()
            {
                IEnumerable<ExpensesVM> exp_list = null;
                IEnumerable<Expenses> exp;
                exp = _expRepo.GetAll();
          
                if (exp != null)
                {

                exp_list = _mapper.Map<IEnumerable<Expenses>, IEnumerable<ExpensesVM>>(exp);
                
                    return View(exp_list);
                }
                return null;
            }

            [HttpGet]
            public ActionResult CreateExp()
            {
            var cat = _expCatRepo.GetAll();
            CreateEditExpensesVM model = new CreateEditExpensesVM()
            {
                Categories = new SelectList(cat, "Id", "Category"),
            };
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult CreateExp(CreateEditExpensesVM model)
            {
                if (ModelState.IsValid)
                {
                    var exp = _mapper.Map<CreateEditExpensesVM, Expenses>(model);
                    _expRepo.Create(exp.ExpCategoryId, exp.Name, exp.Cost);
                    return RedirectToAction("ExpList");
                }
                return View(model);
            }


            public ActionResult DeleteExp(int? id)
            {
                return (id == null || !_expRepo.DeleteById(id))
                   ? NotFound()
                   : RedirectToAction("ExpList");
            }

            [HttpGet]
            public ActionResult EditExp(int id)
            {
                var selectedExp = _expRepo.GetById(id);
                var cat = _expCatRepo.GetAll();
                var selectedViewModel = _mapper.Map<Expenses, CreateEditExpensesVM>(selectedExp);
                selectedViewModel.Categories = new SelectList(cat, "Id", "Category", selectedExp.ExpCategoryId);
                return View(selectedViewModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult EditExp(CreateEditExpensesVM model)
            {
                if (ModelState.IsValid)
                {
                    var exp = _mapper.Map<CreateEditExpensesVM, Expenses>(model);
                    _expRepo.Update(exp.Id, exp.ExpCategoryId, exp.Name, exp.Cost);
                    return RedirectToAction("ExpList");
                }

            var cat = _expCatRepo.GetAll();
            model.Categories = new SelectList(cat, "ExpCategoryId", "Category", model.ExpCategoryId);
            return RedirectToAction("ExpList");
            }

        public DataTable GetExpCat()
        {

            var expenses = this._expRepo.GetAll();
            DataTable table = null;
            if (expenses.Count() != 0)
            {
                var query = expenses
                             .GroupBy(p => new { p.ExpCategoryId })
                             .Select(g => new { cat = g.Key.ExpCategoryId, sum = g.Sum(x => x.Cost) }); 

                DataTable dt = new DataTable();
                dt.Columns.Add("Category", System.Type.GetType("System.String"));
                dt.Columns.Add("Sum", System.Type.GetType("System.Int32"));

                Func<object, DataRow> createRow = (object data) =>
                {
                    var row = dt.NewRow();
                    row.ItemArray = data.GetType().GetProperties().Select(a => a.GetValue(data)).ToArray();
                    return row;
                };

                var rows = from i in query
                           select createRow(new { catgr = this._expCatRepo.GetById(i.cat).Category, sum = i.sum });
                table = rows.CopyToDataTable<DataRow>();
            }
            return table;
        }

        [HttpPost]
        // Išlaidų pagal kategorijas grafiko duomenys
        public JsonResult ExpCal()
        {
          
            var table = this.GetExpCat();
            if (table != null)
            {
                List<object> iData = new List<object>();
                if (table.Rows.Count != 0)
                {
                    foreach (DataColumn dc in table.Columns)
                    {
                        List<object> x = new List<object>();
                        x = (from DataRow drr in table.Rows select drr[dc.ColumnName]).ToList();
                        iData.Add(x);
                    }
                }
                return Json(iData);
            }
            else
            {
                return null;
            }
        }

        // Grafikas
        public PartialViewResult ExpStructure()
        {
          return PartialView("_ExpStructure");
        }
    }
    }
