namespace eDoc_Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStepIndexStepTAble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Step", "StepIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Step", "StepIndex");
        }
    }
}
