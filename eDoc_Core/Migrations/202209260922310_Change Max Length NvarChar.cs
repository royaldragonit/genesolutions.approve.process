namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMaxLengthNvarChar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Document", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Document", "Description", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
