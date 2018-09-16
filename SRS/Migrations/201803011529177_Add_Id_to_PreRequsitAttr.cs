namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Id_to_PreRequsitAttr : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblPreRequis");
            AddColumn("dbo.tblPreRequis", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId", "PreRquisCourseId", "PreRquisCourseDepId", "id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblPreRequis");
            DropColumn("dbo.tblPreRequis", "id");
            AddPrimaryKey("dbo.tblPreRequis", new[] { "CourseId", "CourseDepId", "PreRquisCourseId", "PreRquisCourseDepId" });
        }
    }
}
