namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColTotblRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblRegis", "Senester", c => c.String(nullable: false));
            AddColumn("dbo.tblRegis", "RegisterCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblRegis", "RegisterCreated");
            DropColumn("dbo.tblRegis", "Senester");
        }
    }
}
