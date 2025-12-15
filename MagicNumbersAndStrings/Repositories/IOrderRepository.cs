using MagicNumbersAndStrings.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicNumbersAndStrings.Repositories
{
    public interface IOrderRepository
    {
        string ProccessOrder(Order ord);
    }
}
