namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentIDDataTypeAspUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblUser", "StudentID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblUser", "StudentID", c => c.String());
        }
    }
}
