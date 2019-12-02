using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ViewModel
{
    public class ProductEditViewModel : ProductViewModel
    {
        public List<string> StImagePath { get; set; }
        public string ExistenceImagePath { get; set; }
    }
}