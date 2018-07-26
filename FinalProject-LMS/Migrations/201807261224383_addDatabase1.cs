namespace FinalProject_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDatabase1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        TypeId = c.String(nullable: false, maxLength: 128),
                        ModuleId = c.String(nullable: false, maxLength: 128),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("dbo.ActivityTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CourseId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "CourseId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CourseId");
            AddForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Activities", "TypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Modules", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "CourseId" });
            DropIndex("dbo.Modules", new[] { "CourseId" });
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropIndex("dbo.Activities", new[] { "TypeId" });
            DropColumn("dbo.AspNetUsers", "CourseId");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Courses");
            DropTable("dbo.Modules");
            DropTable("dbo.Activities");
        }
    }
}
