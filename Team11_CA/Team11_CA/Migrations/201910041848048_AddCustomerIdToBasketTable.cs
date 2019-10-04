namespace Team11_CA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdToBasketTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Baskets", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Baskets", "CustomerId");
        }
    }
}
