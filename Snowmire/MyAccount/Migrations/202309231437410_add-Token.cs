namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestTables", "Token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestTables", "Token");
        }
    }
}
