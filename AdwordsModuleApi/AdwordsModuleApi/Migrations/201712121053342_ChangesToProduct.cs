namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesToProduct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.KeyValuePairs", newName: "KeyValuePairLoes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.KeyValuePairLoes", newName: "KeyValuePairs");
        }
    }
}
