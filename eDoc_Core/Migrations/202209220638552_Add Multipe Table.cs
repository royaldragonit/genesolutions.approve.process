namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultipeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        TypeDocumentId = c.Int(nullable: false),
                        ApproveProcessId = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.ApproveProcess", t => t.ApproveProcessId, cascadeDelete: true)
                .ForeignKey("dbo.TypeDocument", t => t.TypeDocumentId, cascadeDelete: true)
                .Index(t => t.TypeDocumentId)
                .Index(t => t.ApproveProcessId);
            
            CreateTable(
                "dbo.DocumentFile",
                c => new
                    {
                        DocumentFileId = c.Int(nullable: false, identity: true),
                        DocumentId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 200),
                        FileNameInServer = c.String(maxLength: 300),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentFileId)
                .ForeignKey("dbo.Document", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "TypeDocumentId", "dbo.TypeDocument");
            DropForeignKey("dbo.DocumentFile", "DocumentId", "dbo.Document");
            DropForeignKey("dbo.Document", "ApproveProcessId", "dbo.ApproveProcess");
            DropIndex("dbo.DocumentFile", new[] { "DocumentId" });
            DropIndex("dbo.Document", new[] { "ApproveProcessId" });
            DropIndex("dbo.Document", new[] { "TypeDocumentId" });
            DropTable("dbo.DocumentFile");
            DropTable("dbo.Document");
        }
    }
}
