namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultipeTableFix2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "Email");
        }
    }
}
