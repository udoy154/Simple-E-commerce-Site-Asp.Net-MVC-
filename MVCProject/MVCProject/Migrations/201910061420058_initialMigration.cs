namespace MVCProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ParentCatID = c.Long(),
                        Name = c.String(),
                        DisplayOrder = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CategoryID = c.Long(nullable: false),
                        Brand = c.String(),
                        Name = c.String(),
                        Details = c.String(),
                        Price = c.Double(nullable: false),
                        StockQuantity = c.Double(nullable: false),
                        IsFavorite = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductMGs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        ImgPath = c.String(),
                        ThumbnailPath = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        IsDefaultImg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMGs", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ProductMGs", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.ProductMGs");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
