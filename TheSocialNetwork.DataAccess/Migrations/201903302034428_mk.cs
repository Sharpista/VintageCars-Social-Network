namespace TheSocialNetwork.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Posts", name: "Group_Id", newName: "MarketPlace_Id");
            RenameColumn(table: "dbo.ProfileGroups", name: "Group_Id", newName: "MarketPlace_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_Group_Id", newName: "IX_MarketPlace_Id");
            RenameIndex(table: "dbo.ProfileGroups", name: "IX_Group_Id", newName: "IX_MarketPlace_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProfileGroups", name: "IX_MarketPlace_Id", newName: "IX_Group_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_MarketPlace_Id", newName: "IX_Group_Id");
            RenameColumn(table: "dbo.ProfileGroups", name: "MarketPlace_Id", newName: "Group_Id");
            RenameColumn(table: "dbo.Posts", name: "MarketPlace_Id", newName: "Group_Id");
        }
    }
}
