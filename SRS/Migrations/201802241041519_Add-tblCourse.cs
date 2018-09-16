namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCourse",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AspDepartmentId = c.Int(nullable: false),
                        CourseName = c.String(nullable: false, maxLength: 150),
                        CourseTitle = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AspDepartmentId })
                .ForeignKey("dbo.tblDep", t => t.AspDepartmentId, cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblCourse", "AspDepartmentId", "dbo.tblDep");
            DropTable("dbo.tblCourse");
        }
    }
}
