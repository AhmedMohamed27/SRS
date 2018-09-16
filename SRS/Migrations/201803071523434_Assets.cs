namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assets : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblUser");
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblUser");
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId", "AuthKey" });
        }
    }
}
