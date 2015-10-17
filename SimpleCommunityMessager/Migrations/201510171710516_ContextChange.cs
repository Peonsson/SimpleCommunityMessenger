namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Receiver_Id = c.String(maxLength: 128),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Receiver_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Receiver_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Sender_Id" });
            DropIndex("dbo.Posts", new[] { "Receiver_Id" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Posts");
        }
    }
}
