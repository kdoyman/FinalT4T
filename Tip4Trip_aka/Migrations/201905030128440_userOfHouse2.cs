namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userOfHouse2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Houses", "OwnerR_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Houses", new[] { "OwnerR_Id" });
            DropColumn("dbo.Houses", "OwnerR_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Houses", "OwnerR_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Houses", "OwnerR_Id");
            AddForeignKey("dbo.Houses", "OwnerR_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
