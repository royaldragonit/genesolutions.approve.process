namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableApproveDocument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApproveDocument",
                c => new
                    {
                        ApproveDocumentId = c.Int(nullable: false, identity: true),
                        DocumentId = c.Int(nullable: false),
                        ApproveId = c.Int(nullable: false),
                        StepId = c.Int(nullable: false),
                        StateApprove = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ApproveDocumentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApproveDocument");
        }
    }
}
