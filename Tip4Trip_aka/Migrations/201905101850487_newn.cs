namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "RenterId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "RenterId");
            AddForeignKey("dbo.Reservations", "RenterId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Reservations", "renter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "renter", c => c.String());
            DropForeignKey("dbo.Reservations", "RenterId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "RenterId" });
            DropColumn("dbo.Reservations", "RenterId");
        }
    }
}
