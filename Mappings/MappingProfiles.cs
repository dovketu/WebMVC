using AutoMapper;
using WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Mappings
{
    public class MappingProfiles
    {
        public class ExpCategoryProfile : Profile
        {
            public ExpCategoryProfile()
            {
                CreateMap<ExpCategory, ExpCategoryListView>();
                CreateMap<ExpCategory, CreatEditExpCategoryVM>();
               
                CreateMap<ExpCategoryListView, ExpCategory>();
                CreateMap<CreatEditExpCategoryVM, ExpCategory>();

            }
        }

        public class ExpensesProfile : Profile
        {
            public ExpensesProfile()
            {
                CreateMap<ExpensesVM, Expenses>();
                CreateMap<CreateEditExpensesVM, Expenses>();
                CreateMap<Expenses, ExpensesVM>();
                CreateMap<Expenses, CreateEditExpensesVM>();
                    
            }
        }

    }
}

