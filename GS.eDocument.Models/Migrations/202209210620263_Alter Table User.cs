﻿namespace GS.eDocument.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String(maxLength: 100));
        }
    }
}
