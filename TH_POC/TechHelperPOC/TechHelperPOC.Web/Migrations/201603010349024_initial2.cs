namespace TechHelperPOC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SystemInfoes", "ScannedDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SystemInfoes", "ScannedDate");
        }
    }
}
