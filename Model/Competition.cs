﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
