using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    // Išlaidų kategorijų duomenys
    public class ExpCategoryListView
    {
        public int Id { get; set; }

        [DisplayName("Expense category")]
        public string Category { get; set; }
    }

    public class CreatEditExpCategoryVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Insert name")]
        [DisplayName("Expense name")]
        public string Category { get; set; }
    }

    // Išlaidų duomenys
    public class ExpensesVM
    {
        public int Id { get; set; }

        [DisplayName("Expense name")]
        public string Name { get; set; }

        [DisplayName("Cost, Eur.")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }
    }

    public class CreateEditExpensesVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Select Category")]
        [Display(Name = "Category")]
        public int ExpCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [DisplayName("Expense name")]
        [Required(ErrorMessage = "Insert expense name")]
        public string Name { get; set; }

        [DisplayName("Cost, Eur")]
        [Required(ErrorMessage = "Insert cost, Eur")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [Range(0.1, Double.MaxValue, ErrorMessage = "Insert cost, Eur")]
        public decimal Cost { get; set; }
    }
}
