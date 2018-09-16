namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_tblPrequisCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId" }, "dbo.tblCourse");
            DropForeignKey("dbo.tblPreRequis", new[] { "PreRquisCourseId", "PreRquisCourseDepId" }, "dbo.tblCourse");
            DropIndex("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId" });
            DropIndex("dbo.tblPreRequis", new[] { "PreRquisCourseId", "PreRquisCourseDepId" });
            DropTable("dbo.tblPreRequis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblPreRequis",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        CourseDepId = c.Int(nullable: false),
                        PreRquisCourseId = c.Int(nullable: false),
                        PreRquisCourseDepId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.CourseDepId, t.PreRquisCourseId, t.PreRquisCourseDepId });
            
            CreateIndex("dbo.tblPreRequis", new[] { "PreRquisCourseId", "PreRquisCourseDepId" });
            CreateIndex("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId" });
            AddForeignKey("dbo.tblPreRequis", new[] { "PreRquisCourseId", "PreRquisCourseDepId" }, "dbo.tblCourse", new[] { "CourseId", "AspDepartmentId" }, cascadeDelete: true);
            AddForeignKey("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId" }, "dbo.tblCourse", new[] { "CourseId", "AspDepartmentId" }, cascadeDelete: true);
        }
    }
}
