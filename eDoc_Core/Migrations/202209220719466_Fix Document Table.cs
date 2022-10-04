namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDocumentTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Document", "DocumentTypeName");
            DropColumn("dbo.Document", "ApproveProcessName");
            DropColumn("dbo.Document", "FileQuantity");
            DropColumn("dbo.Document", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Document", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Document", "FileQuantity", c => c.Int());
            AddColumn("dbo.Document", "ApproveProcessName", c => c.String());
            AddColumn("dbo.Document", "DocumentTypeName", c => c.String());
        }
    }
}
