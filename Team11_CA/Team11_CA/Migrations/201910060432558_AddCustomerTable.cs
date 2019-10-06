namespace Team11_CA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
            "dbo.Customers",
            c => new
            {
                Id = c.String(nullable: false, maxLength: 128),
                Username = c.String(),
                Password = c.String(),
                FirstName = c.String(),
                LastName = c.String(),
                Email = c.String(),
                PhoneNumber = c.Int(),
                Address = c.String(),
                PostalCode = c.String()
            })
            .PrimaryKey(t => t.Id);
    }

        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
