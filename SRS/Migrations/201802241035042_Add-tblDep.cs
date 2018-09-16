namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblDep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblDep",
                c => new
                    {
                        DepId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.DepId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblDep");
        }
    }
}
