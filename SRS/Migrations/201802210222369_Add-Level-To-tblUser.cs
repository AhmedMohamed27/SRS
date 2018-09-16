namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLevelTotblUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUser", "Level", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblUser", "Level");
        }
    }
}
