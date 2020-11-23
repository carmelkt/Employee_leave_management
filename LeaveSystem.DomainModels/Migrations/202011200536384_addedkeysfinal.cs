namespace LeaveSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedkeysfinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Leaves", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Leaves", new[] { "DepartmentID" });
            AddColumn("dbo.Employees", "RoleID", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "RoleID");
            CreateIndex("dbo.Employees", "DepartmentID");
            AddForeignKey("dbo.Employees", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            DropColumn("dbo.Leaves", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leaves", "DepartmentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Employees", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "DepartmentID" });
            DropIndex("dbo.Employees", new[] { "RoleID" });
            DropColumn("dbo.Employees", "DepartmentID");
            DropColumn("dbo.Employees", "RoleID");
            CreateIndex("dbo.Leaves", "DepartmentID");
            AddForeignKey("dbo.Leaves", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
        }
    }
}
