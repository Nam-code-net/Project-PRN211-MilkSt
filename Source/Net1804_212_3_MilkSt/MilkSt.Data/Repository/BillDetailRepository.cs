using MilkSt.Data.Base;
using MilkSt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkSt.Data.Repository
{
    public class BillDetailRepository: GenericRepository<BillDetail>
    {
        public BillDetailRepository()
        {

        }

        public BillDetailRepository(Net1804_212_3_MilkStContext context) => _context = context;
    }
}
