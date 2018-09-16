namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assets2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblRegis", "Senester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblRegis", "Senester", c => c.String(nullable: false));
        }
    }
}
