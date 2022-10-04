namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableApproveDocumentFixDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApproveDocument", "Description", c => c.String());
            AddColumn("dbo.ApproveDocument", "StepIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Document", "IsApprove", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ApproveDocument", "DocumentId");
            AddForeignKey("dbo.ApproveDocument", "DocumentId", "dbo.Document", "DocumentId", cascadeDelete: true);
            DropColumn("dbo.ApproveDocument", "ApproveId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApproveDocument", "ApproveId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApproveDocument", "DocumentId", "dbo.Document");
            DropIndex("dbo.ApproveDocument", new[] { "DocumentId" });
            DropColumn("dbo.Document", "IsApprove");
            DropColumn("dbo.ApproveDocument", "StepIndex");
            DropColumn("dbo.ApproveDocument", "Description");
        }
    }
}
