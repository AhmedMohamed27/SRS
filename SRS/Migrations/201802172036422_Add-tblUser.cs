namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUser",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AspRoleId = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 120),
                        Password = c.String(nullable: false),
                        Phone = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => new { t.UserId, t.AspRoleId })
                .ForeignKey("dbo.tblRole", t => t.AspRoleId, cascadeDelete: false)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUser", "AspRoleId", "dbo.tblRole");
            DropIndex("dbo.tblUser", new[] { "Email" });
            DropIndex("dbo.tblUser", new[] { "AspRoleId" });
            DropTable("dbo.tblUser");
        }
    }
}
