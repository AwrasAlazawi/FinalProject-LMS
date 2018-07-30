namespace FinalProject_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.Activities", "Type_Id", "dbo.ActivityTypes");
            DropIndex("dbo.Activities", new[] { "Module_Id" });
            DropIndex("dbo.Activities", new[] { "Type_Id" });
            DropColumn("dbo.Activities", "ModuleId");
            DropColumn("dbo.Activities", "TypeId");
            RenameColumn(table: "dbo.Activities", name: "Module_Id", newName: "ModuleId");
            RenameColumn(table: "dbo.Activities", name: "Type_Id", newName: "TypeId");
            AlterColumn("dbo.Activities", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Activities", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Activities", "TypeId");
            CreateIndex("dbo.Activities", "ModuleId");
            AddForeignKey("dbo.Activities", "ModuleId", "dbo.Modules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Activities", "TypeId", "dbo.ActivityTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "TypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropIndex("dbo.Activities", new[] { "TypeId" });
            AlterColumn("dbo.Activities", "TypeId", c => c.Int());
            AlterColumn("dbo.Activities", "ModuleId", c => c.Int());
            AlterColumn("dbo.Activities", "ModuleId", c => c.String(nullable: false));
            AlterColumn("dbo.Activities", "TypeId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Activities", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Activities", name: "ModuleId", newName: "Module_Id");
            AddColumn("dbo.Activities", "TypeId", c => c.String(nullable: false));
            AddColumn("dbo.Activities", "ModuleId", c => c.String(nullable: false));
            CreateIndex("dbo.Activities", "Type_Id");
            CreateIndex("dbo.Activities", "Module_Id");
            AddForeignKey("dbo.Activities", "Type_Id", "dbo.ActivityTypes", "Id");
            AddForeignKey("dbo.Activities", "Module_Id", "dbo.Modules", "Id");
        }
    }
}
