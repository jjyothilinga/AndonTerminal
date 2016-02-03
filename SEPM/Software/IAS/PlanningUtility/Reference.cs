using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanningUtility
{
    public class Reference
    {
        public int ID {get;set;}
        public String ReferenceNo { get; set; }
        public int Line { get; set; }
        public String Description { get; set; }

        public Reference()
        {
        }

        public Reference(int id, String number, int line, String description)
        {
            ID = id;
            ReferenceNo = number;
            Line = line;
            Description = description;
        }
    }
}
