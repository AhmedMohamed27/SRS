namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentLvlForignTotbkUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblUser", "AspStudentLvlId", c => c.Int(nullable: false));
            AddForeignKey("dbo.tblUser", "AspStudentLvlId", "dbo.tblStudentLvl", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUser", "AspStudentLvlId", "dbo.tblStudentLvl");
            DropColumn("dbo.tblUser", "AspStudentLvlId");
        }
    }
}
