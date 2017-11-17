namespace AdwordsModuleApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComeOn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNumber = c.String(),
                        ProductName = c.String(),
                        LocigName = c.String(),
                        Description = c.String(),
                        ExtraDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
