using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ViewModel
{
    public class CategoryDetailsViewModel : CategoryViewModel
    {
        public List<Product> Products { get; set; }
    }
}