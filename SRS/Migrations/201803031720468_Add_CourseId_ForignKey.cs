namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CourseId_ForignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblMatrial", "AspCourseId", c => c.Int(nullable: false));
            AddColumn("dbo.tblMatrial", "AspDepId", c => c.Int(nullable: false));
            AddForeignKey("dbo.tblMatrial", new[] { "AspCourseId", "AspDepId" }, "dbo.tblCourse", new[] { "CourseId", "AspDepartmentId" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblMatrial", new[] { "AspCourseId", "AspDepId" }, "dbo.tblCourse");
            DropColumn("dbo.tblMatrial", "AspDepId");
            DropColumn("dbo.tblMatrial", "AspCourseId");
        }
    }
}
