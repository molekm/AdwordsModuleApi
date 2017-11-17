namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionOnLogicName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LogicName", c => c.String());
            DropColumn("dbo.Products", "LocigName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "LocigName", c => c.String());
            DropColumn("dbo.Products", "LogicName");
        }
    }
}
