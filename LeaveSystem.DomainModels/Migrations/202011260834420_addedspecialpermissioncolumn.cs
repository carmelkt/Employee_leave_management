namespace LeaveSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedspecialpermissioncolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "IsSpecialPermission", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "IsManager");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "IsManager", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "IsSpecialPermission");
        }
    }
}
