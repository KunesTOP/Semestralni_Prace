using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.Classes
{
    public class ZvireMaNemoc
    {
        public int NemocNemocId { get; set; }
        public int ZvireIdZvire { get; set; }
        public override string ToString()
        {
            return $"NemocNemocId: {NemocNemocId}, ZvireIdZvire: {ZvireIdZvire}";
        }
    }
}
