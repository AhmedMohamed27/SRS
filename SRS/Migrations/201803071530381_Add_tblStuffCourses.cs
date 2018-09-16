namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_tblStuffCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblStuffCourses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                    CourseId = c.Int(nullable: false),
                    DepId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblCourse", t => new { t.CourseId, t.DepId }, cascadeDelete: true)
                .ForeignKey("dbo.tblUser", t => new { t.UserId, t.RoleId }, cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblStuffCourses", new[] { "UserId", "RoleId" }, "dbo.tblUser");
            DropForeignKey("dbo.tblStuffCourses", new[] { "CourseId", "DepId" }, "dbo.tblCourse");
            DropIndex("dbo.tblStuffCourses", new[] { "CourseId", "DepId" });
            DropIndex("dbo.tblStuffCourses", new[] { "UserId", "RoleId" });
            DropTable("dbo.tblStuffCourses");
        }
    }
}
