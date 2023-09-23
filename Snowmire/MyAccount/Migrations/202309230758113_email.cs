namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestTables", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestTables", "Email");
        }
    }
}
