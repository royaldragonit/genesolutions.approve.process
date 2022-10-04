namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableParamForEmailTemplate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParamEmailTemplate",
                c => new
                    {
                        ParamEmailTemplateId = c.Int(nullable: false, identity: true),
                        ParamName = c.String(maxLength: 200),
                        Description = c.String(maxLength: 200),
                        EmailTemplateId = c.Int(nullable: false),
                        ParamValue = c.String(maxLength: 1000),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ParamEmailTemplateId)
                .ForeignKey("dbo.EmailTemplate", t => t.EmailTemplateId, cascadeDelete: true)
                .Index(t => t.EmailTemplateId);
            
            AddColumn("dbo.DocumentFile", "FileNameOriginal", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParamEmailTemplate", "EmailTemplateId", "dbo.EmailTemplate");
            DropIndex("dbo.ParamEmailTemplate", new[] { "EmailTemplateId" });
            DropColumn("dbo.DocumentFile", "FileNameOriginal");
            DropTable("dbo.ParamEmailTemplate");
        }
    }
}
