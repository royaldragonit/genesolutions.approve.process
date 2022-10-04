namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStepColumnToDocumentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "Step", c => c.Int(nullable: false));
            AddColumn("dbo.Document", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Document", "DocumentTypeName", c => c.String());
            AddColumn("dbo.Document", "ApproveProcessName", c => c.String());
            AddColumn("dbo.Document", "FileQuantity", c => c.Int());
            AddColumn("dbo.Document", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "Discriminator");
            DropColumn("dbo.Document", "FileQuantity");
            DropColumn("dbo.Document", "ApproveProcessName");
            DropColumn("dbo.Document", "DocumentTypeName");
            DropColumn("dbo.Document", "IsCompleted");
            DropColumn("dbo.Document", "Step");
        }
    }
}
