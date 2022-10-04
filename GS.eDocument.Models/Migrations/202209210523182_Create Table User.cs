namespace GS.eDocument.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        MicrosoftId = c.String(maxLength: 100),
                        Username = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        Fullname = c.String(maxLength: 100),
                        Birthday = c.DateTime(nullable: false),
                        Phone = c.String(maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
