namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestoreApproveProcessIdforDocumentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Document", "ApproveProcessId", "dbo.ApproveProcess");
            DropIndex("dbo.Document", new[] { "ApproveProcessId" });
            AddColumn("dbo.TypeDocument", "ApproveProcessId", c => c.Int(nullable: false));
            AlterColumn("dbo.Document", "Description", c => c.String(nullable: false, maxLength: 1000));
            CreateIndex("dbo.TypeDocument", "ApproveProcessId");
            AddForeignKey("dbo.TypeDocument", "ApproveProcessId", "dbo.ApproveProcess", "ApproveProcessId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeDocument", "ApproveProcessId", "dbo.ApproveProcess");
            DropIndex("dbo.TypeDocument", new[] { "ApproveProcessId" });
            AlterColumn("dbo.Document", "Description", c => c.String(maxLength: 1000));
            DropColumn("dbo.TypeDocument", "ApproveProcessId");
            CreateIndex("dbo.Document", "ApproveProcessId");
            AddForeignKey("dbo.Document", "ApproveProcessId", "dbo.ApproveProcess", "ApproveProcessId", cascadeDelete: true);
        }
    }
}
