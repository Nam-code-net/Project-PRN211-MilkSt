using MilkSt.Data.Base;
using MilkSt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkSt.Data.Repository
{
    public class BillRepository: GenericRepository<Bill>
    {
        public BillRepository() { }

        public BillRepository(Net1804_212_3_MilkStContext context) => _context = context;
    }
}
