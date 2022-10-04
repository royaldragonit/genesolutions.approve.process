namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddisAllAccept : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Step", "IsAllAccept", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Step", "IsAllAccept");
        }
    }
}
