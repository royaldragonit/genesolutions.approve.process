namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogExceptionTable : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.LogException");
        }
    }
}
