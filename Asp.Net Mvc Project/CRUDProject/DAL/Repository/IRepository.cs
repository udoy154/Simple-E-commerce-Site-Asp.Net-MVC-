using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDB.WebForm.PJ1.DAL
{
    interface IRepository<T> where T : class
    {
        T Add(T svm);
        T Get(long? id);
        IEnumerable<T> GetAll();
        T Update(T svm);
        bool Remove(long id);
        bool Remove(T svm);
    }//i
}//ns