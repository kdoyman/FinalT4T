namespace Tip4Trip_aka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1305_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Rolename");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Rolename", c => c.String());
        }
    }
}
