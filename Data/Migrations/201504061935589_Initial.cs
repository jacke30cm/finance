namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AchievementAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Achievement_Id = c.Int(),
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
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        Gender = c.String(),
                        SignUp = c.DateTime(),
                        SignOut = c.DateTime(),
                        BirthDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Image_Id = c.Int(),
                        Ranking_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .ForeignKey("dbo.Rankings", t => t.Ranking_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Image_Id)
                .Index(t => t.Ranking_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Contests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContestType = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        AmountOfParticipants = c.Int(nullable: false),
                        CashLimit = c.Int(nullable: false),
                        Administrator_Id = c.String(maxLength: 128),
                        Image_Id = c.Int(),
                        Settings_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Administrator_Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .ForeignKey("dbo.ContestSettings", t => t.Settings_Id)
                .Index(t => t.Administrator_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Settings_Id);
            
            CreateTable(
                "dbo.ContestSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisiblePortfolios = c.Boolean(nullable: false),
                        VisibleScore = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_Id = c.Int(),
                        Share_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .ForeignKey("dbo.Shares", t => t.Share_Id)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.Share_Id);
            
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Balance = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PortfolioHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Change = c.Double(nullable: false),
                        NetWorth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Time = c.DateTime(nullable: false),
                        Portfolio_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .Index(t => t.Portfolio_Id);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Ticker = c.String(),
                        Market = c.String(),
                        Description = c.String(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ShareHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChangePercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeCash = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ask = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Highest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Lowest = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeStamp = c.DateTime(nullable: false),
                        Share_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shares", t => t.Share_Id)
                .Index(t => t.Share_Id);
            
            CreateTable(
                "dbo.UserContestPortfolioAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contest_Id = c.Int(),
                        Portfolio_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contests", t => t.Contest_Id)
                .ForeignKey("dbo.Portfolios", t => t.Portfolio_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Contest_Id)
                .Index(t => t.Portfolio_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserContestPortfolioAssociations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserContestPortfolioAssociations", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.UserContestPortfolioAssociations", "Contest_Id", "dbo.Contests");
            DropForeignKey("dbo.ShareHistories", "Share_Id", "dbo.Shares");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Transactions", "Share_Id", "dbo.Shares");
            DropForeignKey("dbo.Shares", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Transactions", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.PortfolioHistories", "Portfolio_Id", "dbo.Portfolios");
            DropForeignKey("dbo.Contests", "Settings_Id", "dbo.ContestSettings");
            DropForeignKey("dbo.Contests", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Contests", "Administrator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementAssociations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Ranking_Id", "dbo.Rankings");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AchievementAssociations", "Achievement_Id", "dbo.Achievements");
            DropForeignKey("dbo.Achievements", "Image_Id", "dbo.Images");
            DropIndex("dbo.UserContestPortfolioAssociations", new[] { "User_Id" });
            DropIndex("dbo.UserContestPortfolioAssociations", new[] { "Portfolio_Id" });
            DropIndex("dbo.UserContestPortfolioAssociations", new[] { "Contest_Id" });
            DropIndex("dbo.ShareHistories", new[] { "Share_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Shares", new[] { "Country_Id" });
            DropIndex("dbo.PortfolioHistories", new[] { "Portfolio_Id" });
            DropIndex("dbo.Transactions", new[] { "Share_Id" });
            DropIndex("dbo.Transactions", new[] { "Portfolio_Id" });
            DropIndex("dbo.Contests", new[] { "Settings_Id" });
            DropIndex("dbo.Contests", new[] { "Image_Id" });
            DropIndex("dbo.Contests", new[] { "Administrator_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Ranking_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Image_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Achievements", new[] { "Image_Id" });
            DropIndex("dbo.AchievementAssociations", new[] { "User_Id" });
            DropIndex("dbo.AchievementAssociations", new[] { "Achievement_Id" });
            DropTable("dbo.UserContestPortfolioAssociations");
            DropTable("dbo.ShareHistories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Shares");
            DropTable("dbo.PortfolioHistories");
            DropTable("dbo.Portfolios");
            DropTable("dbo.Transactions");
            DropTable("dbo.Countries");
            DropTable("dbo.ContestSettings");
            DropTable("dbo.Contests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Rankings");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Images");
            DropTable("dbo.Achievements");
            DropTable("dbo.AchievementAssociations");
        }
    }
}
