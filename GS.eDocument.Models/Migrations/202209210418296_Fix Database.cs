namespace GS.eDocument.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDatabase : DbMigration
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
                        UserId = c.Int(nullable: false),
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
                        ApproverWith = c.String(),
                        ApproveProcessId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.ApproveProcess", t => t.ApproveProcessId, cascadeDelete: true)
                .Index(t => t.ApproveProcessId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Step", "ApproveProcessId", "dbo.ApproveProcess");
            DropIndex("dbo.Step", new[] { "ApproveProcessId" });
            DropTable("dbo.Step");
            DropTable("dbo.ApproveProcess");
        }
    }
}
