namespace MedievalShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        ClientType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientTypes", t => t.ClientType, cascadeDelete: true)
                .Index(t => t.ClientType);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                        Price = c.Int(nullable: false),
                        ItemType = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemTypes", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ItemTypeEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemTypes", t => t.ItemTypeEntity_Id)
                .Index(t => t.ItemTypeEntity_Id);
            
            CreateTable(
                "dbo.ClientTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "ClientType", "dbo.ClientTypes");
            DropForeignKey("dbo.Items", "Type_Id", "dbo.ItemTypes");
            DropForeignKey("dbo.ItemTypes", "ItemTypeEntity_Id", "dbo.ItemTypes");
            DropForeignKey("dbo.Purchases", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Purchases", "ClientId", "dbo.Clients");
            DropIndex("dbo.ItemTypes", new[] { "ItemTypeEntity_Id" });
            DropIndex("dbo.Items", new[] { "Type_Id" });
            DropIndex("dbo.Purchases", new[] { "ItemId" });
            DropIndex("dbo.Purchases", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "ClientType" });
            DropTable("dbo.ClientTypes");
            DropTable("dbo.ItemTypes");
            DropTable("dbo.Items");
            DropTable("dbo.Purchases");
            DropTable("dbo.Clients");
        }
    }
}
