namespace LeaveSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedreqtextfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "RequestText", c => c.String());
            DropColumn("dbo.Leaves", "LeavesCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "LeavesCount", c => c.Int(nullable: false));
            DropColumn("dbo.Leaves", "RequestText");
        }
    }
}
