namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paraskeyh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Houses", "Title", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Locations", "NameCity", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Locations", "NameMunicipality", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "NameMunicipality", c => c.String());
            AlterColumn("dbo.Locations", "NameCity", c => c.String(nullable: false));
            AlterColumn("dbo.Houses", "Title", c => c.String(nullable: false));
        }
    }
}
