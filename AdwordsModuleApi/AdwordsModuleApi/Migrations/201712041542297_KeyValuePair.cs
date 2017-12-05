namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KeyValuePair : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KeyValuePairs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        ProductLoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductLoes", t => t.ProductLoId, cascadeDelete: true)
                .Index(t => t.ProductLoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KeyValuePairs", "ProductLoId", "dbo.ProductLoes");
            DropIndex("dbo.KeyValuePairs", new[] { "ProductLoId" });
            DropTable("dbo.KeyValuePairs");
        }
    }
}
