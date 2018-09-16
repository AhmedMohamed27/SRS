namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCourseTitleFromAspCourse : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblCourse", "CourseTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblCourse", "CourseTitle", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
