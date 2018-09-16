namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblDoctorCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDoctorCourses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                    CourseId = c.Int(nullable: false),
                    LvlId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCourse", t => new { t.CourseId, t.LvlId }, cascadeDelete: true)
                .ForeignKey("dbo.tblUser", t => new { t.UserId, t.RoleId }, cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblDoctorCourses", new[] { "UserId", "RoleId" }, "dbo.tblUser");
            DropForeignKey("dbo.tblDoctorCourses", new[] { "CourseId", "LvlId" }, "dbo.tblCourse");
            DropTable("dbo.tblDoctorCourses");
        }
    }
}
