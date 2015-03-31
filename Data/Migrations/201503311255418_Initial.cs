namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Portfolios", "PortfolioOwner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Portfolioassociations", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.Portfolioassociations", "Share_Id", "dbo.Shares");
            DropForeignKey("dbo.UserContestPortfolioAssociations", "Admin_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Portfolioassociations", new[] { "Portfolio_Id" });
            DropIndex("dbo.Portfolioassociations", new[] { "Share_Id" });
            DropIndex("dbo.Portfolios", new[] { "PortfolioOwner_Id" });
            DropIndex("dbo.UserContestPortfolioAssociations", new[] { "Admin_Id" });
            CreateTable(
                "dbo.AchievementAssociations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Achievement_Id = c.Long(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Achievements", t => t.Achievement_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Achievement_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContestSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VisiblePortfolios = c.Boolean(nullable: false),
                        VisibleTrades = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_Id = c.Long(),
                        Share_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .ForeignKey("dbo.Shares", t => t.Share_Id)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.Share_Id);
            
            AddColumn("dbo.Contests", "ContestType", c => c.String());
            AddColumn("dbo.Contests", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Contests", "Settings_Id", c => c.Long());
            AddColumn("dbo.AspNetUsers", "Ranking_Id", c => c.Long());
            AddColumn("dbo.Shares", "Country_Id", c => c.Long());
            AddColumn("dbo.ShareHistories", "ChangePercent", c => c.Double(nullable: false));
            AddColumn("dbo.ShareHistories", "ChangeCash", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShareHistories", "Bid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShareHistories", "Ask", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShareHistories", "Latest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShareHistories", "Highest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ShareHistories", "Lowest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PortfolioHistories", "Portfolio_Id", c => c.Long());
            AlterColumn("dbo.Contests", "CashLimit", c => c.Int(nullable: false));
            AlterColumn("dbo.PortfolioHistories", "NetWorth", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.AspNetUsers", "Ranking_Id");
            CreateIndex("dbo.Contests", "Settings_Id");
            CreateIndex("dbo.PortfolioHistories", "Portfolio_Id");
            CreateIndex("dbo.Shares", "Country_Id");
            AddForeignKey("dbo.AspNetUsers", "Ranking_Id", "dbo.Rankings", "Id");
            AddForeignKey("dbo.Contests", "Settings_Id", "dbo.ContestSettings", "Id");
            AddForeignKey("dbo.PortfolioHistories", "Portfolio_Id", "dbo.Portfolios", "Id");
            AddForeignKey("dbo.Shares", "Country_Id", "dbo.Countries", "Id");
            DropColumn("dbo.Portfolios", "PortfolioOwner_Id");
            DropColumn("dbo.Shares", "ListName");
            DropColumn("dbo.Shares", "ChangeToday");
            DropColumn("dbo.Shares", "ChangeTodayCash");
            DropColumn("dbo.Shares", "PurchasePrice");
            DropColumn("dbo.Shares", "SellPrice");
            DropColumn("dbo.Shares", "Latest");
            DropColumn("dbo.Shares", "Highest");
            DropColumn("dbo.Shares", "Lowest");
            DropColumn("dbo.ShareHistories", "Sell");
            DropColumn("dbo.ShareHistories", "Buy");
            DropColumn("dbo.ShareHistories", "Change");
            DropColumn("dbo.ShareHistories", "SellAmount");
            DropColumn("dbo.ShareHistories", "BuyAmount");
            DropColumn("dbo.UserContestPortfolioAssociations", "Admin_Id");
            DropTable("dbo.Portfolioassociations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Portfolioassociations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Portfolio_Id = c.Long(),
                        Share_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserContestPortfolioAssociations", "Admin_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ShareHistories", "BuyAmount", c => c.Int(nullable: false));
            AddColumn("dbo.ShareHistories", "SellAmount", c => c.Int(nullable: false));
            AddColumn("dbo.ShareHistories", "Change", c => c.Double(nullable: false));
            AddColumn("dbo.ShareHistories", "Buy", c => c.Double(nullable: false));
            AddColumn("dbo.ShareHistories", "Sell", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "Lowest", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "Highest", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "Latest", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "SellPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "PurchasePrice", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "ChangeTodayCash", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "ChangeToday", c => c.Double(nullable: false));
            AddColumn("dbo.Shares", "ListName", c => c.String());
            AddColumn("dbo.Portfolios", "PortfolioOwner_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Transactions", "Share_Id", "dbo.Shares");
            DropForeignKey("dbo.Shares", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Transactions", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.PortfolioHistories", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.Contests", "Settings_Id", "dbo.ContestSettings");
            DropForeignKey("dbo.AchievementAssociations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Ranking_Id", "dbo.Rankings");
            DropForeignKey("dbo.AchievementAssociations", "Achievement_Id", "dbo.Achievements");
            DropForeignKey("dbo.Achievements", "Image_Id", "dbo.Images");
            DropIndex("dbo.Shares", new[] { "Country_Id" });
            DropIndex("dbo.PortfolioHistories", new[] { "Portfolio_Id" });
            DropIndex("dbo.Transactions", new[] { "Share_Id" });
            DropIndex("dbo.Transactions", new[] { "Portfolio_Id" });
            DropIndex("dbo.Contests", new[] { "Settings_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Ranking_Id" });
            DropIndex("dbo.Achievements", new[] { "Image_Id" });
            DropIndex("dbo.AchievementAssociations", new[] { "User_Id" });
            DropIndex("dbo.AchievementAssociations", new[] { "Achievement_Id" });
            AlterColumn("dbo.PortfolioHistories", "NetWorth", c => c.Double(nullable: false));
            AlterColumn("dbo.Contests", "CashLimit", c => c.Double(nullable: false));
            DropColumn("dbo.PortfolioHistories", "Portfolio_Id");
            DropColumn("dbo.ShareHistories", "Lowest");
            DropColumn("dbo.ShareHistories", "Highest");
            DropColumn("dbo.ShareHistories", "Latest");
            DropColumn("dbo.ShareHistories", "Ask");
            DropColumn("dbo.ShareHistories", "Bid");
            DropColumn("dbo.ShareHistories", "ChangeCash");
            DropColumn("dbo.ShareHistories", "ChangePercent");
            DropColumn("dbo.Shares", "Country_Id");
            DropColumn("dbo.AspNetUsers", "Ranking_Id");
            DropColumn("dbo.Contests", "Settings_Id");
            DropColumn("dbo.Contests", "Active");
            DropColumn("dbo.Contests", "ContestType");
            DropTable("dbo.Transactions");
            DropTable("dbo.Countries");
            DropTable("dbo.ContestSettings");
            DropTable("dbo.Rankings");
            DropTable("dbo.Achievements");
            DropTable("dbo.AchievementAssociations");
            CreateIndex("dbo.UserContestPortfolioAssociations", "Admin_Id");
            CreateIndex("dbo.Portfolios", "PortfolioOwner_Id");
            CreateIndex("dbo.Portfolioassociations", "Share_Id");
            CreateIndex("dbo.Portfolioassociations", "Portfolio_Id");
            AddForeignKey("dbo.UserContestPortfolioAssociations", "Admin_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Portfolioassociations", "Share_Id", "dbo.Shares", "Id");
            AddForeignKey("dbo.Portfolioassociations", "Portfolio_Id", "dbo.Portfolios", "Id");
            AddForeignKey("dbo.Portfolios", "PortfolioOwner_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
