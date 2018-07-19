using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleJSON
{
    public class DirectionsInfo
    {
        public List<routes> routes { get; set; }
    }

    public class routes
    {
        public List<legs> legs { get; set; } //legs list
    }

    public class distance
    {
        public string text { get; set; }
    }

    public class duration
    {
        public string text { get; set; }
    }

    public class duration_in_traffic
    {
        public string text { get; set; }
    }

    public class legs
    {
        public string end_address { get; set; }
        public string start_address { get; set; } 
        public distance distance { get; set; }
        public duration duration { get; set; }
        public duration_in_traffic duration_In_Traffic { get; set; }
    }
}
