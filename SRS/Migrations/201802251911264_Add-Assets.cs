namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssets : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblCourse");
            AlterColumn("dbo.tblCourse", "CourseId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblCourse", new[] { "CourseId", "AspDepartmentId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblCourse");
            AlterColumn("dbo.tblCourse", "CourseId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tblCourse", new[] { "CourseId", "AspDepartmentId" });
        }
    }
}
