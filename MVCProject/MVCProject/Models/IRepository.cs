using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public interface IRepository<T> where T : class
    {
        T Add(T svm);
        T Get(long id);
        IEnumerable<T> GetAll();
        T Update(T svm);
        bool Remove(long id);
        bool Remove(T svm);
    }
}
