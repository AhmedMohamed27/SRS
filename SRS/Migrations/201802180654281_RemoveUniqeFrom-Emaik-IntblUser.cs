namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUniqeFromEmaikIntblUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tblUser", new[] { "Email" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.tblUser", "Email", unique: true);
        }
    }
}
