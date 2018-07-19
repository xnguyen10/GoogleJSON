using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleJSON
{
    public class LongLatInfo
    {
        public List<results> results { get; set; }
    }

    public class results
    {
        public string formatted_address { get; set; }
    }
}
