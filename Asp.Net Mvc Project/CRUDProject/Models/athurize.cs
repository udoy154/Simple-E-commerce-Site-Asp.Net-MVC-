using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDProject.Models
{
    public class athurize
    {
        public bool CheckNotLogin()
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckUser()
        {
            var data = HttpContext.Current.Session["Role"].ToString().ToLower();
            if (data != "admin")
            {
                return true;
            }
            return false;
        }
    }
}