namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trith3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HouseImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        housId = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HouseImages");
        }
    }
}
