namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableApproveDocumentFixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApproveDocument", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApproveDocument", "Email");
        }
    }
}
