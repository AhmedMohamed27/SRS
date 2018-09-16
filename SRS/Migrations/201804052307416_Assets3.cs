namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assets3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "Year", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId", "Year" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "Year", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId", "Year" });
        }
    }
}
