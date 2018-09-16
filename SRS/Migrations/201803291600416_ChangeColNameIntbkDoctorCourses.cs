namespace SRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColNameIntbkDoctorCourses : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tblDoctorCourses", name: "LvlId", newName: "DepId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.tblDoctorCourses", name: "DepId", newName: "LvlId");
        }
    }
}
