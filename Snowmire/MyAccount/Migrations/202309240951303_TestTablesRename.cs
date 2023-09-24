namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestTablesRename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TestTables", newName: "UserInfoes");
            AddColumn("dbo.UserInfoes", "Hash", c => c.String());
            AddColumn("dbo.UserInfoes", "Salt", c => c.String());
            DropColumn("dbo.UserInfoes", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "Password", c => c.String());
            DropColumn("dbo.UserInfoes", "Salt");
            DropColumn("dbo.UserInfoes", "Hash");
            RenameTable(name: "dbo.UserInfoes", newName: "TestTables");
        }
    }
}
