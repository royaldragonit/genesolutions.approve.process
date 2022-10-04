namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTemplateEmailTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplate",
                c => new
                    {
                        EmailTemplateId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmailTemplateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailTemplate");
        }
    }
}
