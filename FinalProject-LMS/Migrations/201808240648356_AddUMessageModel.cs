namespace FinalProject_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUMessageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.String(),
                        Rubrik = c.String(),
                        Text = c.String(),
                        ReciverId = c.String(maxLength: 128),
                        IsReaden = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReciverId)
                .Index(t => t.ReciverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UMessages", "ReciverId", "dbo.AspNetUsers");
            DropIndex("dbo.UMessages", new[] { "ReciverId" });
            DropTable("dbo.UMessages");
        }
    }
}
