namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "Group_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Group_Id");
            AddForeignKey("dbo.Posts", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Group_Id", "dbo.Groups");
            DropIndex("dbo.Posts", new[] { "Group_Id" });
            DropColumn("dbo.Posts", "Group_Id");
            DropTable("dbo.Groups");
        }
    }
}
