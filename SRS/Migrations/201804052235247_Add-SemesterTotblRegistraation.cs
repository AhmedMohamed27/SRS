namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSemesterTotblRegistraation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRegis", "Semester", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRegis", "Semester");
        }
    }
}
