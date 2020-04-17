namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditingInviteTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invites", "MeetingInvite_InviteID", "dbo.Invites");
            DropIndex("dbo.Invites", new[] { "MeetingInvite_InviteID" });
            DropColumn("dbo.Invites", "Meeting");
            DropColumn("dbo.Invites", "MeetingInvite_InviteID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invites", "MeetingInvite_InviteID", c => c.Int());
            AddColumn("dbo.Invites", "Meeting", c => c.Int(nullable: false));
            CreateIndex("dbo.Invites", "MeetingInvite_InviteID");
            AddForeignKey("dbo.Invites", "MeetingInvite_InviteID", "dbo.Invites", "InviteID");
        }
    }
}
