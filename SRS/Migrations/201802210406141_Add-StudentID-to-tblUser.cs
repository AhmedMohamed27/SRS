namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentIDtotblUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUser", "StudentID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblUser", "StudentID");
        }
    }
}
