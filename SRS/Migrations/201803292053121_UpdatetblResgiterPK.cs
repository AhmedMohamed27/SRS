namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetblResgiterPK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "Year", c => c.String(nullable: false));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblRegis");
            AlterColumn("dbo.tblRegis", "Year", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.tblRegis", new[] { "RegisID", "UserId", "AspRoleId", "CourseId", "AspDepartmentId", "Year" });
        }
    }
}
