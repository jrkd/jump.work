using Model;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////
            //Db seed
            ///////////////////////////////
            //using (var db = new JumpDBContext())
            //{

            //    Skill reallyClassySnowboarding = new Skill() { Name = "Snowboarding (Very classy)" };
            //    Skill reallyWeirdSnowboarding = new Skill() { Name = "Snowboarding (Very weird)" };
            //    Skill snowboarding = new Skill() { Name = "Snowboarding", SubSkills = new List<Skill>() { reallyClassySnowboarding, reallyWeirdSnowboarding } };
            //    Skill winterSports = new Skill() { Name = "Winter sports", SubSkills = new List<Skill>() { snowboarding } };
            //    Skill outdoorSports = new Skill() { Name = "Outdoor sports", SubSkills = new List<Skill>() { winterSports } };

            //    Skill eSports = new Skill() { Name = "eSports" };
            //    Skill indoorSports = new Skill() { Name = "Indoor Sports", SubSkills = new List<Skill>() { eSports } };

            //    db.Skills.Add(reallyWeirdSnowboarding);
            //    db.Skills.Add(reallyClassySnowboarding);
            //    db.Skills.Add(snowboarding);
            //    db.Skills.Add(winterSports);
            //    db.Skills.Add(outdoorSports);

            //    db.Skills.Add(eSports);
            //    db.Skills.Add(indoorSports);

            //    Competition olympics15 = new Competition() { Name = "Olympics", Year = 2015 };
            //    Competition olympics14 = new Competition() { Name = "Olympics", Year = 2014 };
            //    db.Competitions.Add(olympics14);
            //    db.Competitions.Add(olympics15);

            //    db.People.Add(new Person()
            //    {
            //        Name = "jono",
            //        age = 22,
            //        Competitions = new List<Competition> { olympics14 },
            //        Skills = new List<Skill> { snowboarding }
            //    });

            //    db.SaveChanges();
            //}


            //////////////////////
            //Search testing
            //////////////////////
            System.Console.WriteLine("--Search test 1--");
            IEnumerable<Person> results = Search.SearchHelper.searchForPeople(new List<int>() { 4 }, "jono", 0, 100, 0);
            foreach( var person in results)
            {
                System.Console.WriteLine("Name: " + person.Name);
                System.Console.WriteLine("Age: " + person.age);
                System.Console.WriteLine("Skills: " + string.Join(",", person.Skills.Select(skill => skill.Name)));
                System.Console.WriteLine("Relevant Subskills (1level): " + string.Join(",", person.Skills.SelectMany(skill => skill.SubSkills).Select(skill=>skill.Name)));
                System.Console.WriteLine("Comps: " + string.Join(",", person.Competitions.Select(comp => comp.Name + " (" + comp.Year + ")")));
            }
        }
    }
}
    