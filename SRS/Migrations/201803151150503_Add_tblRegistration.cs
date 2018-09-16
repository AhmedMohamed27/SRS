namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_tblRegistration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRegis",
                c => new
                {
                    RegisID = c.Int(nullable: false),
                    UserId = c.Int(nullable: false),
                    AspRoleId = c.Int(nullable: false),
                    CourseId = c.Int(nullable: false),
                    AspDepartmentId = c.Int(nullable: false),
                    Year = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => new { t.RegisID, t.UserId, t.AspRoleId, t.CourseId, t.AspDepartmentId, t.Year })
                .ForeignKey("dbo.tblCourse", t => new { t.CourseId, t.AspDepartmentId }, cascadeDelete: false)
                .ForeignKey("dbo.tblUser", t => new { t.UserId, t.AspRoleId }, cascadeDelete: false);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblRegis", new[] { "UserId", "AspRoleId" }, "dbo.tblUser");
            DropForeignKey("dbo.tblRegis", new[] { "CourseId", "AspDepartmentId" }, "dbo.tblCourse");
            DropTable("dbo.tblRegis");
        }
    }
}
