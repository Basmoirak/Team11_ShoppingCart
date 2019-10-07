namespace Team11_CA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Team11_CA.Shop.Core.Library;
    using Team11_CA.Shop.Core.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Team11_CA.Shop.DataAccess.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Team11_CA.Shop.DataAccess.DataContext context)
        {
            //Uncomment code below for initial Migration

            PasswordHash hash = new PasswordHash();

            context.Customers.AddOrUpdate(x => x.Id,
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "customer1",
                    Password = hash.HashPassword("customer1"),
                    FirstName = "Jason",
                    LastName = "Smith",
                    Email = "JasonSmith@issemail.com",
                    PhoneNumber = 123456789,
                    Address = "Heng Mui Keng ISS",
                    PostalCode = "123456",
                },
                new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = "customer2",
                    Password = hash.HashPassword("customer2"),
                    FirstName = "David",
                    LastName = "Garrett",
                    Email = "DavidG@issemail.com",
                    PhoneNumber = 234567810,
                    Address = "Heng Mui Keng ISS",
                    PostalCode = "123456",
                });

            context.Products.AddOrUpdate(x => x.Id,
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Microsoft Word",
                    Description = "The Word app from Microsoft lets you create, edit, view, and share your files with others quickly and easily.",
                    Price = 50,
                    Image = "MicrosoftWord.png"
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Microsoft Excel",
                    Description = "The Excel spreadsheet app lets you create, view, edit, and share your files with others quickly and easily.",
                    Price = 60,
                    Image = "MicrosoftExcel.png"
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Microsoft Powerpoint",
                    Description = "The PowerPoint app gives you access to the familiar tool you already know.",
                    Price = 40,
                    Image = "MicrosoftPowerpoint.png"
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Microsoft OneNote",
                    Description = "Organize your thoughts, discoveries, and ideas and simplify planning with your digital notepad.",
                    Price = 50,
                    Image = "MicrosoftOneNote.png"
                },
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Microsoft OneDrive",
                    Description = "The OneDrive app lets you easily work with your personal and work files when you’re on the go.",
                    Price = 75,
                    Image = "MicrosoftOneDrive.png"
                }
                );
        }
    }
}
