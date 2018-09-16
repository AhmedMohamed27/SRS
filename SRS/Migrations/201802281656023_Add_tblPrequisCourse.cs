namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_tblPrequisCourse : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => new { t.CourseId, t.CourseDepId, t.PreRquisCourseId, t.PreRquisCourseDepId })
                .ForeignKey("dbo.tblCourse", t => new { t.CourseId, t.CourseDepId }, cascadeDelete: false)
                .ForeignKey("dbo.tblCourse", t => new { t.PreRquisCourseId, t.PreRquisCourseDepId }, cascadeDelete: false);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblPreRequis", new[] { "PreRquisCourseId", "PreRquisCourseDepId" }, "dbo.tblCourse");
            DropForeignKey("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId" }, "dbo.tblCourse");
            DropTable("dbo.tblPreRequis");
        }
    }
}
