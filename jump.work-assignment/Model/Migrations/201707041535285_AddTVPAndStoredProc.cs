namespace Model
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTVPAndStoredProc : DbMigration
    {
        public override void Up()
        {
            this.Sql(Properties.Resources.CREATE_TYPE_integer_list_tbltype);
            this.Sql(Properties.Resources.CREATE_STORED_PROC_GetSubSkills);
        }
        
        public override void Down()
        {
        }
    }
}
