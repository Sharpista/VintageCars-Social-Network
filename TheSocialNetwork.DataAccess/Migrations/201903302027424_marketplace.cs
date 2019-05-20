namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marketplace : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MarketPlaces", newName: "MarketPlaces");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MarketPlaces", newName: "MarketPlaces");
        }
    }
}
