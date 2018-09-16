namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthKeyToAspUser : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblUser");
            AddColumn("dbo.tblUser", "AuthKey", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId", "AuthKey" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblUser");
            DropColumn("dbo.tblUser", "AuthKey");
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId" });
        }
    }
}
