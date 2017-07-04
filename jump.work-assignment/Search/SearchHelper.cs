using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Search
{
    public class SearchHelper 
    {
        /// <summary>
        /// Handles search queries against data model looking for people with specific attributes:
        /// </summary>
        /// <param name="withSkills"></param>
        /// <param name="likeName"></param>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <param name="yearsExperience"></param>
        /// <returns></returns>
        public static IEnumerable<Person> searchForPeople(IEnumerable<int> withSkills, string likeName, int? minAge, int? maxAge, int yearsExperience)
        {
            IQueryable<Person> peopleToFilter = null;
            IEnumerable<Person> filteredPeople = new List<Person>();
            using (var db = new JumpDBContext())
            {
                peopleToFilter = db.People_AllData;
                peopleToFilter = filterPeopleByProperties(likeName, minAge, maxAge, ref peopleToFilter); //TODO: CHECK THIS - might not work.ToList();
                peopleToFilter = filterPeopleByYearsExperience(yearsExperience, ref peopleToFilter);

                //After initial filtering, process the remaining by skill
                IDictionary<int, IEnumerable<int>> childSkillsByRequired = db.GetSubSkillIdsByParent(withSkills);
                filteredPeople = peopleToFilter.ToList(); //This forces immediate execution of query on db. 
                filteredPeople = filterPeopleBySkills(withSkills, childSkillsByRequired, filteredPeople);
            }
            return filteredPeople;
        }

        /// <summary>
        /// Filters by basic properties. More complex properties are processed separately.
        /// NB: SQL escaping happens for free with EF6 which internally escapes & uses SQLParams.
        /// </summary>
        /// <param name="likeName"></param>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <param name="peopleToFilter"></param>
        /// <returns></returns>
        private static IQueryable<Person> filterPeopleByProperties(string likeName, int? minAge, int? maxAge, ref IQueryable<Person> peopleToFilter)
        {
            peopleToFilter.Where(person => person.Name.Contains(likeName));
            if (minAge.HasValue)
            {
                peopleToFilter = peopleToFilter.Where(person => person.age >= minAge);
            }
            if (maxAge.HasValue)
            {
                peopleToFilter = peopleToFilter.Where(person => person.age <= maxAge);
            }
            return peopleToFilter;
        }
        

        /// <summary>
        /// A few details on calcuating competitions:
        /// - Your number of years experience is the number of years between now and your earliest event.
        /// - If you have no events, then you have 0 years experience
        /// - The search returns those with the same OR MORE years of experience.
        /// </summary>
        /// <param name="yearsExperience">num years exp to filter by -- filters out those with less years exp.</param>
        /// <param name="peopleToFilter">existing query to be run against database.</param>
        /// <returns></returns>
        private static IQueryable<Person> filterPeopleByYearsExperience(int yearsExperience, ref IQueryable<Person> peopleToFilter)
        {
            int currentYear = DateTime.Today.Year;
            peopleToFilter = peopleToFilter.Where(person => (currentYear -
                                                    (person.Competitions.Select(comp => comp.Year) //int[] of years
                                                    .DefaultIfEmpty(currentYear).Min()))  //select the earliest year, (or current year if theres no compititions)
                                                >= yearsExperience);
            return peopleToFilter;
        }

        /// <summary>
        /// Different to the other filters, this method needs the existing results already processed by the database 
        /// (See peopleToFilter IEnermable vs IQueryable)
        /// This is so we can process the results individually to manage the skill checking. 
        /// </summary>
        /// <param name="requiredSkills">the explicitly required skills</param>
        /// <param name="childSkillsByRequired">any children a required skill might have, hashed by each required skill</param>
        /// <param name="peopleToFilter">the existing list of people to filter</param>
        /// <returns></returns>
        private static IEnumerable<Person> filterPeopleBySkills(IEnumerable<int> requiredSkills, IDictionary<int, IEnumerable<int>> childSkillsByRequired, IEnumerable<Person> peopleToFilter)
        {
            List<Person> results = new List<Person>();
            foreach (Person person in peopleToFilter) 
            {
                //Find the skills that the person doesnt explicitly have. 
                IEnumerable<int> personSkills = person.Skills.Select(skill => skill.ID);

                //We need to make sure ALL of the requiredSkills are matched; 
                IEnumerable<int> missingRequiredSkills = requiredSkills.Except(person.Skills.Select(skill => skill.ID));

                if(missingRequiredSkills.Count() == 0) //initial check when the person has all skills explicitly
                {
                    results.Add(person);
                    continue;
                }
                else
                {
                    //But if they dont all match explcitly, 
                    //they can be matched implicitly by matching with ANY of their children
                    bool hasAllMissingSkillsAsChildren = true;
                    foreach (int skillID in missingRequiredSkills)
                    {   
                        //First check that a required skill has any children
                        if (childSkillsByRequired.ContainsKey(skillID))
                        {
                            //then check if the person's skills match with any children of the required skill
                            IEnumerable<int> children = childSkillsByRequired[skillID];
                            if (!personSkills.Intersect(children).Any())
                            {
                                hasAllMissingSkillsAsChildren = false;
                                break;
                            }
                        }
                        else
                        {
                            hasAllMissingSkillsAsChildren = false;
                            break;
                        }
                    }

                    if (hasAllMissingSkillsAsChildren)
                    {
                        results.Add(person);
                    }
                }
            }

            return results;
        }
    }
}