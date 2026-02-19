using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal interface IRepository<T> where T : class
    {
        void Add(T item);
        void Remove(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        T Update(int id, T item);
    }
}
