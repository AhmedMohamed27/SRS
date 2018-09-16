namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblCourseTime",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblCourseTime");
        }
    }
}
