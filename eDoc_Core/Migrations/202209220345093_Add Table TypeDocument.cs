namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTypeDocument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApproveProcess",
                c => new
                    {
                        ApproveProcessId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 2000),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApproveProcessId);
            
            CreateTable(
                "dbo.Step",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RollBackToStep = c.Int(nullable: false),
                        ApproveProcessId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.ApproveProcess", t => t.ApproveProcessId, cascadeDelete: true)
                .Index(t => t.ApproveProcessId);
            
            CreateTable(
                "dbo.Approve",
                c => new
                    {
                        ApproveId = c.Int(nullable: false, identity: true),
                        StepId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 1000),
                        IsApprove = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ApproveId)
                .ForeignKey("dbo.Step", t => t.StepId, cascadeDelete: true)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.TypeDocument",
                c => new
                    {
                        TypeDocumentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TypeDocumentId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        MicrosoftId = c.String(maxLength: 100),
                        Username = c.String(maxLength: 50),
                        Password = c.String(maxLength: 150),
                        Email = c.String(maxLength: 100),
                        Fullname = c.String(maxLength: 100),
                        Birthday = c.DateTime(),
                        Phone = c.String(maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Approve", "StepId", "dbo.Step");
            DropForeignKey("dbo.Step", "ApproveProcessId", "dbo.ApproveProcess");
            DropIndex("dbo.Approve", new[] { "StepId" });
            DropIndex("dbo.Step", new[] { "ApproveProcessId" });
            DropTable("dbo.User");
            DropTable("dbo.TypeDocument");
            DropTable("dbo.Approve");
            DropTable("dbo.Step");
            DropTable("dbo.ApproveProcess");
        }
    }
}
