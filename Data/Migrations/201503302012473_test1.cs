namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserContests", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserContests", "Contest_Id", "dbo.Contests");
            DropIndex("dbo.UserContests", new[] { "User_Id" });
            DropIndex("dbo.UserContests", new[] { "Contest_Id" });
            AddColumn("dbo.Contests", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contests", "EndDate", c => c.DateTime(nullable: false));
            DropTable("dbo.UserContests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserContests",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Contest_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Contest_Id });
            
            DropColumn("dbo.Contests", "EndDate");
            DropColumn("dbo.Contests", "StartDate");
            CreateIndex("dbo.UserContests", "Contest_Id");
            CreateIndex("dbo.UserContests", "User_Id");
            AddForeignKey("dbo.UserContests", "Contest_Id", "dbo.Contests", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserContests", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
