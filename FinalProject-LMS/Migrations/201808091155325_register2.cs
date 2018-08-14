namespace FinalProject_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class register2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Kind");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Kind", c => c.Int());
        }
    }
}
