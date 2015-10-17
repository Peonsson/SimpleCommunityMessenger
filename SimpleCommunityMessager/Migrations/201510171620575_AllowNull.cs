namespace SimpleCommunityMessager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime(nullable: false));
        }
    }
}
