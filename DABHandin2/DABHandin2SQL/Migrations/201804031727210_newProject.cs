namespace DABHandin2SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MailAddress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressType = c.String(nullable: false),
                        StreetName = c.String(nullable: false),
                        HouseNumber = c.String(nullable: false),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Company = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AddressPersons",
                c => new
                    {
                        Address_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Address_Id, t.Person_Id })
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Address_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.PersonEmails",
                c => new
                    {
                        Person_Id = c.Int(nullable: false),
                        Email_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Person_Id, t.Email_Id })
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.Email_Id, cascadeDelete: true)
                .Index(t => t.Person_Id)
                .Index(t => t.Email_Id);
            
            CreateTable(
                "dbo.PhoneNumberPersons",
                c => new
                    {
                        PhoneNumber_Id = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhoneNumber_Id, t.Person_Id })
                .ForeignKey("dbo.PhoneNumbers", t => t.PhoneNumber_Id, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.PhoneNumber_Id)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhoneNumberPersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.PhoneNumberPersons", "PhoneNumber_Id", "dbo.PhoneNumbers");
            DropForeignKey("dbo.PersonEmails", "Email_Id", "dbo.Emails");
            DropForeignKey("dbo.PersonEmails", "Person_Id", "dbo.People");
            DropForeignKey("dbo.AddressPersons", "Person_Id", "dbo.People");
            DropForeignKey("dbo.AddressPersons", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.PhoneNumberPersons", new[] { "Person_Id" });
            DropIndex("dbo.PhoneNumberPersons", new[] { "PhoneNumber_Id" });
            DropIndex("dbo.PersonEmails", new[] { "Email_Id" });
            DropIndex("dbo.PersonEmails", new[] { "Person_Id" });
            DropIndex("dbo.AddressPersons", new[] { "Person_Id" });
            DropIndex("dbo.AddressPersons", new[] { "Address_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropTable("dbo.PhoneNumberPersons");
            DropTable("dbo.PersonEmails");
            DropTable("dbo.AddressPersons");
            DropTable("dbo.PhoneNumbers");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
            DropTable("dbo.People");
            DropTable("dbo.Emails");
        }
    }
}
