namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContestUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contests", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contests", "ContestLength", c => c.String());
            AlterColumn("dbo.Contests", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Contests", "EndDate", c => c.DateTime());
            DropColumn("dbo.Contests", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contests", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Contests", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contests", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Contests", "ContestLength");
            DropColumn("dbo.Contests", "CreationDate");
        }
    }
}
