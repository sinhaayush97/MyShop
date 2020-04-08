namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderItems", name: "OrderID", newName: "Order_Id");
            RenameIndex(table: "dbo.OrderItems", name: "IX_OrderID", newName: "IX_Order_Id");
            AddColumn("dbo.OrderItems", "OrdreID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "OrdreID");
            RenameIndex(table: "dbo.OrderItems", name: "IX_Order_Id", newName: "IX_OrderID");
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderID");
        }
    }
}
