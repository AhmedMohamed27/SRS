namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTimeTotblCourses2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCourse", "CourseTimeId", c => c.Int(nullable: false,defaultValue:1));
            AddForeignKey("dbo.tblCourse", "CourseTimeId", "dbo.tblCourseTime", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCourse", "CourseTimeId", "dbo.tblCourseTime");
            DropColumn("dbo.tblCourse", "CourseTimeId");
        }
    }
}
