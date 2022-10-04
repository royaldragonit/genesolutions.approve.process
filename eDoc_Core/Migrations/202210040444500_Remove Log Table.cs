namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLogTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LogException");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LogException",
                c => new
                    {
                        LogExceptionId = c.Int(nullable: false, identity: true),
                        Message = c.String(maxLength: 500),
                        StackTrace = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogExceptionId);
            
        }
    }
}
