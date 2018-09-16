namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStudentIdDataTypeInAspUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblUser", "StudentID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblUser", "StudentID", c => c.Int(nullable: false));
        }
    }
}
