namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedVarro1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VarroCounts", "MiteCount", c => c.Int(nullable: false));
            DropColumn("dbo.VarroCounts", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VarroCounts", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.VarroCounts", "MiteCount");
        }
    }
}
