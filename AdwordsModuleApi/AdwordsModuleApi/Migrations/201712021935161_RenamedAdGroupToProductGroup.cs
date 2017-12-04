namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedAdGroupToProductGroup : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AdGroupLoes", newName: "ProductGroupLoes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProductGroupLoes", newName: "AdGroupLoes");
        }
    }
}
