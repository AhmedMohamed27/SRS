namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUserIDIdentityIntblUser : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblUser");
            AlterColumn("dbo.tblUser", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblUser");
            AlterColumn("dbo.tblUser", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tblUser", new[] { "UserId", "AspRoleId" });
        }
    }
}
