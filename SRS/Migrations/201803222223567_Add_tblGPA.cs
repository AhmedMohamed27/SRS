namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_tblGPA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblGPA",
                c => new
                {
                    GpaId = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                    Takenhoures = c.Int(nullable: false),
                    Pendinghoures = c.Int(nullable: false),
                    GpaVal = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => new { t.GpaId, t.UserId, t.RoleId })
                .ForeignKey("dbo.tblUser", t => new { t.UserId, t.RoleId }, cascadeDelete: false);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblGPA", new[] { "UserId", "RoleId" }, "dbo.tblUser");
            DropTable("dbo.tblGPA");
        }
    }
}
