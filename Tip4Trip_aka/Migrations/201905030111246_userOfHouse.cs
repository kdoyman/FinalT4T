namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userOfHouse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Houses", "OwnerR_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Houses", "OwnerR_Id");
            AddForeignKey("dbo.Houses", "OwnerR_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Houses", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Houses", "Owner", c => c.String());
            DropForeignKey("dbo.Houses", "OwnerR_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Houses", new[] { "OwnerR_Id" });
            DropColumn("dbo.Houses", "OwnerR_Id");
        }
    }
}
