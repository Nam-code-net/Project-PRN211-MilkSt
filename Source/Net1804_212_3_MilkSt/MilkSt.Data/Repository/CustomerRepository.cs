using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilkSt.Data.Base;
using MilkSt.Data.Models;

namespace MilkSt.Data.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository() 
        {
            
        }
        public CustomerRepository(Net1804_212_3_MilkStContext context) => _context = context;

    }
}
