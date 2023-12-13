using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ascenda_Exercise
{
    public class Sort
    {
        public void sortListMerchants(List<merchants> merchantsList)
        {
            merchantsList = merchantsList.OrderBy(r => r.distance).ToList();
        }
    }

}

