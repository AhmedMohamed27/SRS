namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTimeTotblCourses1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblCourse", "CourseTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblCourse", "CourseTime", c => c.String(nullable: false));
        }
    }
}
