using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Models
{
    public class Table
    {
        public int ID { get; set; }
        public string TableName { get; set; }
        public bool Available { get; set; }
        public DateTime StartTime { get; set; }
    }
}
