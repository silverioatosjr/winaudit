namespace TechHelperPOC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtableLinuxUdit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Technician = c.String(),
                        Client = c.String(),
                        Site = c.String(),
                        Workstation = c.String(),
                        OS = c.String(),
                        ScannedDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LinuxInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Technician = c.String(nullable: false),
                        Client = c.String(nullable: false),
                        Site = c.String(nullable: false),
                        OS = c.String(nullable: false),
                        MachineName = c.String(nullable: false),
                        ScannedDate = c.String(nullable: false),
                        FileName = c.String(),
                        HtmlFile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SelectedComputers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SelectedComputers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Computer = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.LinuxInfoes");
            DropTable("dbo.Auditors");
        }
    }
}
