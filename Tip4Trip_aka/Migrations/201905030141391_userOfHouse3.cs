namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userOfHouse3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Houses", "Owner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Houses", "Owner_Id");
            AddForeignKey("dbo.Houses", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Houses", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Houses", new[] { "Owner_Id" });
            DropColumn("dbo.Houses", "Owner_Id");
        }
    }
}
