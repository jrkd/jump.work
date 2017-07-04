using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int age { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
