namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultipeTableFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocumentFile", "IsPrimaryFile", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocumentFile", "IsPrimaryFile");
        }
    }
}
