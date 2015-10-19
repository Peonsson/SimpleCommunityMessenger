namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_model_attributes3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupMessages", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupMessages", "Sender_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GroupMessages", new[] { "Group_Id" });
            DropIndex("dbo.GroupMessages", new[] { "Sender_Id" });
            DropIndex("dbo.Posts", new[] { "Group_Id" });
            AlterColumn("dbo.GroupMessages", "Subject", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.GroupMessages", "Message", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.GroupMessages", "Group_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.GroupMessages", "Sender_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "Subject", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "Message", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.GroupMessages", "Group_Id");
            CreateIndex("dbo.GroupMessages", "Sender_Id");
            AddForeignKey("dbo.GroupMessages", "Group_Id", "dbo.Groups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupMessages", "Sender_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Posts", "Group_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Group_Id", c => c.Int());
            DropForeignKey("dbo.GroupMessages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMessages", "Group_Id", "dbo.Groups");
            DropIndex("dbo.GroupMessages", new[] { "Sender_Id" });
            DropIndex("dbo.GroupMessages", new[] { "Group_Id" });
            AlterColumn("dbo.Posts", "Message", c => c.String());
            AlterColumn("dbo.Posts", "Subject", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.GroupMessages", "Sender_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupMessages", "Group_Id", c => c.Int());
            AlterColumn("dbo.GroupMessages", "Message", c => c.String());
            AlterColumn("dbo.GroupMessages", "Subject", c => c.String());
            CreateIndex("dbo.Posts", "Group_Id");
            CreateIndex("dbo.GroupMessages", "Sender_Id");
            CreateIndex("dbo.GroupMessages", "Group_Id");
            AddForeignKey("dbo.GroupMessages", "Sender_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupMessages", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.Posts", "Group_Id", "dbo.Groups", "Id");
        }
    }
}
