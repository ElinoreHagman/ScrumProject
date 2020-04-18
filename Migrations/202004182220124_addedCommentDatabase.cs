namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCommentDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ProfileID = c.String(maxLength: 128),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.ProfileID)
                .Index(t => t.PostID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "ProfileID", "dbo.Profiles");
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "ProfileID" });
            DropTable("dbo.Comments");
        }
    }
}
