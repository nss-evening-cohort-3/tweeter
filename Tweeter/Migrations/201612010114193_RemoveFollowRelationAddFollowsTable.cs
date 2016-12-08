namespace Tweeter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFollowRelationAddFollowsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Twits", "Twit_TwitId", "dbo.Twits");
            DropIndex("dbo.Twits", new[] { "Twit_TwitId" });
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        FollowId = c.Int(nullable: false, identity: true),
                        TwitFollowed_TwitId = c.Int(),
                        TwitFollower_TwitId = c.Int(),
                    })
                .PrimaryKey(t => t.FollowId)
                .ForeignKey("dbo.Twits", t => t.TwitFollowed_TwitId)
                .ForeignKey("dbo.Twits", t => t.TwitFollower_TwitId)
                .Index(t => t.TwitFollowed_TwitId)
                .Index(t => t.TwitFollower_TwitId);
            
            DropColumn("dbo.Twits", "Twit_TwitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Twits", "Twit_TwitId", c => c.Int());
            DropForeignKey("dbo.Follows", "TwitFollower_TwitId", "dbo.Twits");
            DropForeignKey("dbo.Follows", "TwitFollowed_TwitId", "dbo.Twits");
            DropIndex("dbo.Follows", new[] { "TwitFollower_TwitId" });
            DropIndex("dbo.Follows", new[] { "TwitFollowed_TwitId" });
            DropTable("dbo.Follows");
            CreateIndex("dbo.Twits", "Twit_TwitId");
            AddForeignKey("dbo.Twits", "Twit_TwitId", "dbo.Twits", "TwitId");
        }
    }
}
