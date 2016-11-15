namespace Tweeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Twits", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Twits", "Username");
        }
    }
}
