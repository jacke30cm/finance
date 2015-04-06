namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contests", "Image_Id", c => c.Int());
            AddColumn("dbo.ContestSettings", "VisibleScore", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Contests", "Image_Id");
            AddForeignKey("dbo.Contests", "Image_Id", "dbo.Images", "Id");
            DropColumn("dbo.ContestSettings", "VisibleTrades");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContestSettings", "VisibleTrades", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Contests", "Image_Id", "dbo.Images");
            DropIndex("dbo.Contests", new[] { "Image_Id" });
            DropColumn("dbo.ContestSettings", "VisibleScore");
            DropColumn("dbo.Contests", "Image_Id");
        }
    }
}
