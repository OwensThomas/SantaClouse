using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClouse
{
    public interface IDataBase
    {
            // Qui si definiscono i metodi che agiscono sul database getAll, update

            bool UpdateOrder(Order category);

            bool UpdateToy(Toy toy);

            IEnumerable<Order> GetAllOreders();

            IEnumerable<Toy> GetAllToys();

            User GetUser(User user);

            Order GetOrder(string id);
        }
    }
