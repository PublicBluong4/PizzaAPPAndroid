using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bluong4_Project2V4.Models
{
    public class Wallpaper
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public DateTime StartTime { get; set; }
    }
}
