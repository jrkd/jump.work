using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.FrontEnd.ViewModels
{
    public class AgeRangeVM
    {
        public int ID { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}