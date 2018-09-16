namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLevelFromtblUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblUser", "Level");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblUser", "Level", c => c.String(maxLength: 20));
        }
    }
}
