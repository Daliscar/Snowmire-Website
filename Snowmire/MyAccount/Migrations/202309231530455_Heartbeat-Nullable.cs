namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HeartbeatNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TestTables", "LastHeartbeatTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestTables", "LastHeartbeatTime", c => c.DateTime(nullable: false));
        }
    }
}
