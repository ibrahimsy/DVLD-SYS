using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_System.Global_Classes
{
    public static class clsFormat
    {
        public static string DateToShort(DateTime DT)
        {
            return DT.ToString("dd/MMM/yyyy");
        }
    }
}
