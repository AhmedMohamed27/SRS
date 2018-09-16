namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblStudentLvl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblStudentLvl",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        lvlName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblStudentLvl");
        }
    }
}
