using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class Expenses
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ExpCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public virtual ExpCategory ExpCategory { get; set; }
    }
}
