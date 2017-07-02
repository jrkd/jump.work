using Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Search
{
    public class SearchHelper 
    {
        /// <summary>
        /// Handles search queries against data model looking for people with specific attributes:
        /// TODO: create a SearchItem here rather than individual params that we could reuse as a view model if we get to MVC it.
        /// </summary>
        /// <param name="likeName"></param>
        /// <param name="minAge"></param>
        /// <param name="MaxAge"></param>
        /// <param name="requiredSkills"></param>
        /// <param name="yearsExperience"></param>
        /// <returns></returns>
        public static IEnumerable<Person> searchForPeople(string likeName, int minAge, int MaxAge, IEnumerable<int> requiredSkills, int yearsExperience)
        {
            IEnumerable<Person> filteredPeople = null;
            List<Person> results = new List<Person>();
            using (var db = new JumpDBContext())
            {
                //Make initial call to filter out all those that dont match name, age, experience before further skill processing.
                filteredPeople = filterParametersExceptSkill(likeName, minAge, MaxAge, yearsExperience, db).ToList();

                //Retrieve ancestors of required skills as dict<required-skill, list-children-skills>
                IDictionary<int, IEnumerable<int>> requiredSkillChildren = db.GetSubSkillIdsByParent(requiredSkills);

                foreach (Person person in filteredPeople) //not filtered by skill yet
                {
                    //Find the skills that the person doesnt explicitly have. 
                    IEnumerable<int> personSkills = person.Skills.Select(skill => skill.ID);
                    var missingRequiredSkills = requiredSkills.Except(person.Skills.Select(skill => skill.ID));

                    //We need to make sure ALL of the requiredSkills is matched; 
                    //but that can occur implicitly by matching with ANY of their children
                    bool hasAllMissingSkillsAsChildren = true;
                    foreach (int skillID in missingRequiredSkills)
                    {
                        //check if the person's skills match with any children of the required skill
                        if (!personSkills.Intersect(requiredSkillChildren[skillID]).Any())
                        {
                            hasAllMissingSkillsAsChildren = false;
                            break;
                        }
                    }
                    
                    //If the person's skills are all matched explciitly, or some/all are matched implicitly, they are chosen.
                    if (hasAllMissingSkillsAsChildren || missingRequiredSkills.Count() == 0)
                    {
                        results.Add(person);
                    }
                }
            }
            return results;
        }
        
        /// <summary>
        /// Filters by everything but skills. Skills need to be done separately due to skill-hierarchy checking.
        /// NB: SQL escaping happens for free with EF6 which internally escapes & uses SQLParams.
        /// </summary>
        /// <param name="likeName"></param>
        /// <param name="minAge"></param>
        /// <param name="MaxAge"></param>
        /// <param name="yearsExperience"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static IQueryable<Person> filterParametersExceptSkill(string likeName, int minAge, int MaxAge, int yearsExperience, JumpDBContext db)
        {
            return db.People.Where(person => person.Name.Contains(likeName))
                .Where(person => person.age >= minAge && person.age <= MaxAge)
                .Where(person => person.Competitions.Max(comp => comp.Year) - person.Competitions.Min(comp => comp.Year) >= yearsExperience);
        }
    }
}