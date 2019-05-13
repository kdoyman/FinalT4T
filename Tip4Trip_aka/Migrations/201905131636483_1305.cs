namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1305 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Rolename", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Rolename");
        }
    }
}
