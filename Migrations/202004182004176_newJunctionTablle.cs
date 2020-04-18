namespace ScrumProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newJunctionTablle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeetingDateOptionsInvites", "MeetingDateOptions_OptionID", "dbo.MeetingDateOptions");
            DropForeignKey("dbo.MeetingDateOptionsInvites", "Invite_InviteID", "dbo.Invites");
            DropIndex("dbo.MeetingDateOptionsInvites", new[] { "MeetingDateOptions_OptionID" });
            DropIndex("dbo.MeetingDateOptionsInvites", new[] { "Invite_InviteID" });
            CreateTable(
                "dbo.MeetingDateOptionsToInvites",
                c => new
                    {
                        InviteID = c.Int(nullable: false),
                        MeetingDateOptionID = c.Int(nullable: false),
                        MeetingDateOptions_OptionID = c.Int(),
                    })
                .PrimaryKey(t => new { t.InviteID, t.MeetingDateOptionID })
                .ForeignKey("dbo.Invites", t => t.InviteID, cascadeDelete: true)
                .ForeignKey("dbo.MeetingDateOptions", t => t.MeetingDateOptions_OptionID)
                .Index(t => t.InviteID)
                .Index(t => t.MeetingDateOptions_OptionID);
            
            DropTable("dbo.MeetingDateOptionsInvites");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MeetingDateOptionsInvites",
                c => new
                    {
                        MeetingDateOptions_OptionID = c.Int(nullable: false),
                        Invite_InviteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MeetingDateOptions_OptionID, t.Invite_InviteID });
            
            DropForeignKey("dbo.MeetingDateOptionsToInvites", "MeetingDateOptions_OptionID", "dbo.MeetingDateOptions");
            DropForeignKey("dbo.MeetingDateOptionsToInvites", "InviteID", "dbo.Invites");
            DropIndex("dbo.MeetingDateOptionsToInvites", new[] { "MeetingDateOptions_OptionID" });
            DropIndex("dbo.MeetingDateOptionsToInvites", new[] { "InviteID" });
            DropTable("dbo.MeetingDateOptionsToInvites");
            CreateIndex("dbo.MeetingDateOptionsInvites", "Invite_InviteID");
            CreateIndex("dbo.MeetingDateOptionsInvites", "MeetingDateOptions_OptionID");
            AddForeignKey("dbo.MeetingDateOptionsInvites", "Invite_InviteID", "dbo.Invites", "InviteID", cascadeDelete: true);
            AddForeignKey("dbo.MeetingDateOptionsInvites", "MeetingDateOptions_OptionID", "dbo.MeetingDateOptions", "OptionID", cascadeDelete: true);
        }
    }
}
