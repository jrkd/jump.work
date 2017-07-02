using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Skill
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Skill> SubSkills { get; set; }
    }
}
