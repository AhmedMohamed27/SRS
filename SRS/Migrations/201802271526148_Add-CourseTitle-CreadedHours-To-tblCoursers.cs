namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseTitleCreadedHoursTotblCoursers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblCourse", "CourseTitle", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.tblCourse", "CreadedHours", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblCourse", "CreadedHours");
            DropColumn("dbo.tblCourse", "CourseTitle");
        }
    }
}
