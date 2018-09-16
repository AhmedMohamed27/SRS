namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTimeTotblCourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCourse", "CourseTime", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCourse", "CourseTime");
        }
    }
}
