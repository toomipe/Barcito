using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barcito.Persistencia
{
    public interface IRepository<T>
    {
        void Save(T entity);
        T FindById(int id);
        List<T> FindAll();
        bool Update(T entity);
        void Delete(int id);

    }

}
