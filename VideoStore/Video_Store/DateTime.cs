using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Store
{
    public class DateTime : IDateTime
    {
        public System.DateTime Now()
        {
            return System.DateTime.Now;
        }
    }
}
