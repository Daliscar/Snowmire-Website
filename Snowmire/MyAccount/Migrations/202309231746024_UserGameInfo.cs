namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserGameInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGameInfoes",
                c => new
                    {
                        UserGameInfoId = c.Int(nullable: false),
                        PlayerName = c.String(),
                        Coins = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        Characters = c.String(),
                    })
                .PrimaryKey(t => t.UserGameInfoId)
                .ForeignKey("dbo.TestTables", t => t.UserGameInfoId)
                .Index(t => t.UserGameInfoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGameInfoes", "UserGameInfoId", "dbo.TestTables");
            DropIndex("dbo.UserGameInfoes", new[] { "UserGameInfoId" });
            DropTable("dbo.UserGameInfoes");
        }
    }
}
