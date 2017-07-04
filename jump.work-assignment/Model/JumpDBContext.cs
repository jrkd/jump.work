using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SkillSubSkill
    {
        public int ID { get; set; }
        public int SubSkillID { get; set; }

    }
    public class JumpDBContext : DbContext
    {
        public JumpDBContext():base("name=DBConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //List of all data types in database
        public DbSet<Person> People { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Competition> Competitions { get; set; }

        public IQueryable<Person> People_AllData {
            get
            {
                return People.Include(person => person.Skills)
                            .Include(person => person.Competitions);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Setup M2M bindings
            modelBuilder.Entity<Person>()
               .HasMany(person => person.Skills) // A person can have many skills
               .WithMany(skill => skill.People); // skills can have many people that use them

            modelBuilder.Entity<Person>()
               .HasMany(person => person.Competitions) // A perosn may have been to many competitions
               .WithMany(competition => competition.People); // Compeititions can host many people

            modelBuilder.Entity<Skill>()
            .HasMany(skill => skill.SubSkills)
            .WithMany()
            .Map(m =>
            {
                m.MapLeftKey("ID");
                m.MapRightKey("SubSkillID");
                m.ToTable("SkillSubSkill");
            });
        }

        /// <summary>
        /// Formats GetSubSkills results as a dict<parent-skill, list<ancestor-skills>>
        /// </summary>
        /// <param name="withSkills">original parent skills to retrieve results for</param>
        /// <returns></returns>
        public IDictionary<int, IEnumerable<int>> GetSubSkillIdsByParent(IEnumerable<int> withSkills)
        {
            return GetSubSkills(withSkills)
                .GroupBy(s => s.ID)
                .ToDictionary(group => group.Key, group => group.Select(skill => skill.SubSkillID));
        }

        /// <summary>
        /// Querys a stored procedure that traverses skill hierarchy and returns SkillID, SubSkillID
        /// Implementation note - Table valued param required to save us hitting the database for each 
        /// parent skill. 
        /// </summary>
        /// <param name="withSkills"></param>
        /// <returns></returns>
        public IEnumerable<SkillSubSkill> GetSubSkills(IEnumerable<int> withSkills)
        {
            const string TVP_INT_TABLE_TYPE_NAME = "dbo.integer_list_tbltype";
            DataTable tvpSkillIDsTable = new DataTable();
            tvpSkillIDsTable.Columns.Add("ID", typeof(int));
            foreach(var skillID in withSkills)
            {
                tvpSkillIDsTable.Rows.Add(skillID);
            }
            SqlParameter tvpParam = new SqlParameter("@tvpParentSkillIDs", tvpSkillIDsTable);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = TVP_INT_TABLE_TYPE_NAME;
            
            return this.Database.SqlQuery<SkillSubSkill>("GetSubSkills @tvpParentSkillIDs", tvpParam);
        }
    }
}
