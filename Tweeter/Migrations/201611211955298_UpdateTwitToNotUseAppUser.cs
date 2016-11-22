namespace Tweeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTwitToNotUseAppUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Twits", "BaseUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Twits", new[] { "BaseUser_Id" });
            AddColumn("dbo.Twits", "BaseUserId", c => c.String());
            DropColumn("dbo.Twits", "BaseUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Twits", "BaseUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Twits", "BaseUserId");
            CreateIndex("dbo.Twits", "BaseUser_Id");
            AddForeignKey("dbo.Twits", "BaseUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
