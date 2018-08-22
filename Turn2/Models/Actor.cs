using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Turn2.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPlayeble { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Agi { get; set; }
        
    }
}
