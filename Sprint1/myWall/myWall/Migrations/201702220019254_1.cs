namespace myWall.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annotations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Note = c.String(nullable: false, maxLength: 500),
                        DiagramId = c.Int(nullable: false),
                        Votes = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diagrams", t => t.DiagramId, cascadeDelete: true)
                .Index(t => t.DiagramId);
            
            CreateTable(
                "dbo.Diagrams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(nullable: false, maxLength: 500),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, maxLength: 500),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        Votes = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        WallId = c.Int(nullable: false),
                        CallobId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 128),
                        Text = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalloborationCenters", t => t.CallobId, cascadeDelete: true)
                .ForeignKey("dbo.Walls", t => t.WallId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.WallId)
                .Index(t => t.CallobId);
            
            CreateTable(
                "dbo.CalloborationCenters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Tier = c.String(nullable: false, maxLength: 100),
                        CallobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalloborationAccount", t => t.CallobId, cascadeDelete: true)
                .Index(t => t.CallobId);
            
            CreateTable(
                "dbo.CalloborationAccount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Organization = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        Vote = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Walls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Post_Tag",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Wall_Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        WallId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.WallId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Walls", t => t.WallId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.WallId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserCalloboration",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CallobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CallobId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.CalloborationAccount", t => t.CallobId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CallobId);
            
            CreateTable(
                "dbo.User_Group",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Post_Diagram",
                c => new
                    {
                        DiagramId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiagramId, t.PostId })
                .ForeignKey("dbo.Diagrams", t => t.DiagramId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.DiagramId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post_Diagram", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Post_Diagram", "DiagramId", "dbo.Diagrams");
            DropForeignKey("dbo.Walls", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Group", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.User_Group", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Diagrams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCalloboration", "CallobId", "dbo.CalloborationAccount");
            DropForeignKey("dbo.UserCalloboration", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Answers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "WallId", "dbo.Walls");
            DropForeignKey("dbo.Wall_Group", "WallId", "dbo.Walls");
            DropForeignKey("dbo.Wall_Group", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Post_Tag", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Post_Tag", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CallobId", "dbo.CalloborationCenters");
            DropForeignKey("dbo.CalloborationCenters", "CallobId", "dbo.CalloborationAccount");
            DropForeignKey("dbo.Answers", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Annotations", "DiagramId", "dbo.Diagrams");
            DropIndex("dbo.Post_Diagram", new[] { "PostId" });
            DropIndex("dbo.Post_Diagram", new[] { "DiagramId" });
            DropIndex("dbo.User_Group", new[] { "GroupId" });
            DropIndex("dbo.User_Group", new[] { "UserId" });
            DropIndex("dbo.UserCalloboration", new[] { "CallobId" });
            DropIndex("dbo.UserCalloboration", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Wall_Group", new[] { "WallId" });
            DropIndex("dbo.Wall_Group", new[] { "GroupId" });
            DropIndex("dbo.Post_Tag", new[] { "TagId" });
            DropIndex("dbo.Post_Tag", new[] { "PostId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Walls", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.CalloborationCenters", new[] { "CallobId" });
            DropIndex("dbo.Posts", new[] { "CallobId" });
            DropIndex("dbo.Posts", new[] { "WallId" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.Answers", new[] { "PostId" });
            DropIndex("dbo.Answers", new[] { "UserId" });
            DropIndex("dbo.Diagrams", new[] { "UserId" });
            DropIndex("dbo.Annotations", new[] { "DiagramId" });
            DropTable("dbo.Post_Diagram");
            DropTable("dbo.User_Group");
            DropTable("dbo.UserCalloboration");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Wall_Group");
            DropTable("dbo.Post_Tag");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Groups");
            DropTable("dbo.Walls");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.CalloborationAccount");
            DropTable("dbo.CalloborationCenters");
            DropTable("dbo.Posts");
            DropTable("dbo.Answers");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Diagrams");
            DropTable("dbo.Annotations");
        }
    }
}
