namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterMatrialIdName : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblMatrial");
            DropColumn("dbo.tblMatrial", "MatId");
            AddColumn("dbo.tblMatrial", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tblMatrial", "Id");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblMatrial", "MatId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.tblMatrial");
            DropColumn("dbo.tblMatrial", "Id");
            AddPrimaryKey("dbo.tblMatrial", "MatId");
        }
    }
}
