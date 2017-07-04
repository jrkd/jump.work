using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Configuration : DbMigrationsConfiguration<JumpDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Model.JumpDBContext";
        }
        protected override void Seed(JumpDBContext context)
        {
            Debugger.Launch();
            base.Seed(context);
            if (context.Skills.Count() == 0)
            {
                var skills = TestData.SKILL_TREE.Flatten(skill => skill.SubSkills);
                var compsBySkill = TestData.GenerateCompetitions(skills);
                IEnumerable<Competition> comps = compsBySkill.SelectMany(pair => pair.Value);
                context.Skills.AddRange(skills);
                context.Competitions.AddRange(comps);
                context.People.AddRange(TestData.GeneratePeople(10000, compsBySkill));

                context.SaveChanges();
            }
        }
        
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(
            this IEnumerable<T> e,
            Func<T, IEnumerable<T>> f)
        {
            return e.SelectMany(c => f(c).Flatten(f)).Concat(e);
        }
    }
}
