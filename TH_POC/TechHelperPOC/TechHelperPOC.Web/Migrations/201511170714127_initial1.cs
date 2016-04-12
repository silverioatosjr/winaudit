namespace TechHelperPOC.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectedComputers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Computer = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SelectedComputers");
        }
    }
}
