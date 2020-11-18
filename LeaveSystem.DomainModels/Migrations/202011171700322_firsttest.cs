namespace LeaveSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firsttest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Name = c.String(),
                        Mobile = c.String(),
                        IsManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        LeaveStartDate = c.DateTime(nullable: false),
                        LeaveEndDate = c.DateTime(nullable: false),
                        LeaveStatus = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        LeavesCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.DepartmentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Leaves", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Leaves", new[] { "DepartmentID" });
            DropIndex("dbo.Leaves", new[] { "EmployeeID" });
            DropTable("dbo.Leaves");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
