using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Data<T> where T: class
    {
        private readonly List<T> _items;
        public Data()
        {
            _items = new List<T>();
        }
        public void Add(T item)
        {
            _items.Add(item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
        }
public int Count=> _items.Count;
        public List<T> GetAll()=>_items.ToList();
        public T Find(Func<T, bool> predicate)=>_items.FirstOrDefault(predicate);
    }
}
