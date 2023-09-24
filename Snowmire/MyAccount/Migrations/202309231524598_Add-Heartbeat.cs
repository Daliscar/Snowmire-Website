namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeartbeat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestTables", "LastHeartbeatTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestTables", "LastHeartbeatTime");
        }
    }
}
