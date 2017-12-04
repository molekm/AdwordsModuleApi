namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdGroupLoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductLoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNumber = c.String(),
                        ProductName = c.String(),
                        LogicName = c.String(),
                        Description = c.String(),
                        DescriptionShort = c.String(),
                        AdGroupLoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AdGroupLoes", t => t.AdGroupLoId, cascadeDelete: true)
                .Index(t => t.AdGroupLoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLoes", "AdGroupLoId", "dbo.AdGroupLoes");
            DropIndex("dbo.ProductLoes", new[] { "AdGroupLoId" });
            DropTable("dbo.ProductLoes");
            DropTable("dbo.AdGroupLoes");
        }
    }
}
