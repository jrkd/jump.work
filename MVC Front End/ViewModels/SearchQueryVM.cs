using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC.FrontEnd.ViewModels
{
    public class SearchQueryVM
    {
        [DisplayName("Athlete name: ")]
        public string LikeName { get; set; }
        [DisplayName("Min. years experience: ")]
        public int YearsExperience { get; set; }

        [DisplayName("Age range: ")]
        public int SelectedAgeRange { get; set; }
        public IEnumerable<AgeRangeVM> AllAgeRanges { get; set; }

        //has to be string as bootstrap-tagsinput posts name=1,2,3,4 
        //-- MVC usually expects int[] as name=1&name=2...etc
        //public IEnumerable<int> RequiredSkillIDs { get; set; }
        [DisplayName("Athlete skills: ")]
        public string RequiredSkillIDs { get; set; } 
        
        public IEnumerable<SkillVM> AllSkills { get; set; }

        public SearchQueryVM()
        {
            RequiredSkillIDs = string.Empty;
        }
    }
}