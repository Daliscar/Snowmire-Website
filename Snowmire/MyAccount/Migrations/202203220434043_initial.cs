namespace MyAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            Sql("Insert into Roles(Name) Values ('SuperAdmin')");
            Sql("Insert into Roles(Name) Values ('Admin')");
            Sql("Insert into Roles(Name) Values ('SuperUser')");
            Sql("Insert into Roles(Name) Values ('User')");
            Sql("Insert into Users (UserId,UserName,Password,RoleId) Values ('Andrei','Andrei','pwd',1)");
            Sql("Insert into Users (UserId,UserName,Password,RoleId) Values ('Iulian','Iuli1','pwd',2)");
            Sql("Insert into Users (UserId,UserName,Password,RoleId) Values ('Mihai','Mihai','pwd',3)");
            Sql("Insert into Users (UserId,UserName,Password,RoleId) Values ('Basic','User','User',4");

        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
