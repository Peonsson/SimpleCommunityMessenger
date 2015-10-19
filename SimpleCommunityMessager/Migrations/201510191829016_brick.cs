namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brick : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MulticastPosts", newName: "GroupMessages");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.GroupMessages", newName: "MulticastPosts");
        }
    }
}
