namespace GS.eDocument.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changerequirement : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.ApproveProcess", "Email", c => c.String());
            DropColumn("dbo.ApproveProcess", "UserId");
            DropColumn("dbo.Step", "ApproverWith");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Step", "ApproverWith", c => c.String());
            AddColumn("dbo.ApproveProcess", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Approve", "StepId", "dbo.Step");
            DropIndex("dbo.Approve", new[] { "StepId" });
            DropColumn("dbo.ApproveProcess", "Email");
            DropTable("dbo.Approve");
        }
    }
}
