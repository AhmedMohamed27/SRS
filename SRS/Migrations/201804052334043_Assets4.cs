namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assets4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "RegisID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId", "Year" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "RegisID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId", "Year" });
        }
    }
}
