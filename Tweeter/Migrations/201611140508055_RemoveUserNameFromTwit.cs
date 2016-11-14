namespace Tweeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserNameFromTwit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Twits", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Twits", "UserName", c => c.String());
        }
    }
}
