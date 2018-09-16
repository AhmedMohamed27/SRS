namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSomeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblCourse", "MatrialId", "dbo.tblMatrial");
            DropIndex("dbo.tblCourse", new[] { "MatrialId" });
            DropColumn("dbo.tblCourse", "MatrialId");
            DropTable("dbo.tblMatrial");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblMatrial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tblCourse", "MatrialId", c => c.Int(nullable: false));
            CreateIndex("dbo.tblCourse", "MatrialId");
            AddForeignKey("dbo.tblCourse", "MatrialId", "dbo.tblMatrial", "Id", cascadeDelete: true);
        }
    }
}
