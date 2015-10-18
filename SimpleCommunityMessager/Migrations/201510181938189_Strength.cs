namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Strength : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MulticastPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Message = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        Group_Id = c.Int(),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MulticastPosts", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MulticastPosts", "Group_Id", "dbo.Groups");
            DropIndex("dbo.MulticastPosts", new[] { "Sender_Id" });
            DropIndex("dbo.MulticastPosts", new[] { "Group_Id" });
            DropTable("dbo.MulticastPosts");
        }
    }
}
