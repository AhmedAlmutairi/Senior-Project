namespace myWall.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annotation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Note = c.String(nullable: false, maxLength: 500),
                        DiagramId = c.Int(nullable: false),
                        Votes = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diagram", t => t.DiagramId, cascadeDelete: true)
                .Index(t => t.DiagramId);
            
            CreateTable(
                "dbo.Diagram",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(nullable: false, maxLength: 500),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        WallId = c.Int(nullable: false),
                        CallobId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Contents = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalloborationCenter", t => t.CallobId, cascadeDelete: true)
                .ForeignKey("dbo.Wall", t => t.WallId)
                .Index(t => t.WallId)
                .Index(t => t.CallobId);
            
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, maxLength: 500),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                        Votes = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.CalloborationCenter",
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
                "dbo.UserCalloboration",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CallobId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CallobId })
                .ForeignKey("dbo.CalloborationAccount", t => t.CallobId, cascadeDelete: true)
                .Index(t => t.CallobId);
            
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
                .ForeignKey("dbo.Post", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wall",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Group",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Post_Tag",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
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
                .ForeignKey("dbo.Wall", t => t.WallId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.WallId);
            
            CreateTable(
                "dbo.Post_Diagram",
                c => new
                    {
                        DiagramId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiagramId, t.PostId })
                .ForeignKey("dbo.Diagram", t => t.DiagramId, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.DiagramId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post_Diagram", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post_Diagram", "DiagramId", "dbo.Diagram");
            DropForeignKey("dbo.Post", "WallId", "dbo.Wall");
            DropForeignKey("dbo.Wall_Group", "WallId", "dbo.Wall");
            DropForeignKey("dbo.Wall_Group", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.User_Group", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Post_Tag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Post_Tag", "PostId", "dbo.Post");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "CallobId", "dbo.CalloborationCenter");
            DropForeignKey("dbo.UserCalloboration", "CallobId", "dbo.CalloborationAccount");
            DropForeignKey("dbo.CalloborationCenter", "CallobId", "dbo.CalloborationAccount");
            DropForeignKey("dbo.Answer", "PostId", "dbo.Post");
            DropForeignKey("dbo.Annotation", "DiagramId", "dbo.Diagram");
            DropIndex("dbo.Post_Diagram", new[] { "PostId" });
            DropIndex("dbo.Post_Diagram", new[] { "DiagramId" });
            DropIndex("dbo.Wall_Group", new[] { "WallId" });
            DropIndex("dbo.Wall_Group", new[] { "GroupId" });
            DropIndex("dbo.Post_Tag", new[] { "TagId" });
            DropIndex("dbo.Post_Tag", new[] { "PostId" });
            DropIndex("dbo.User_Group", new[] { "GroupId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.UserCalloboration", new[] { "CallobId" });
            DropIndex("dbo.CalloborationCenter", new[] { "CallobId" });
            DropIndex("dbo.Answer", new[] { "PostId" });
            DropIndex("dbo.Post", new[] { "CallobId" });
            DropIndex("dbo.Post", new[] { "WallId" });
            DropIndex("dbo.Annotation", new[] { "DiagramId" });
            DropTable("dbo.Post_Diagram");
            DropTable("dbo.Wall_Group");
            DropTable("dbo.Post_Tag");
            DropTable("dbo.User_Group");
            DropTable("dbo.Groups");
            DropTable("dbo.Wall");
            DropTable("dbo.Tag");
            DropTable("dbo.Comments");
            DropTable("dbo.UserCalloboration");
            DropTable("dbo.CalloborationAccount");
            DropTable("dbo.CalloborationCenter");
            DropTable("dbo.Answer");
            DropTable("dbo.Post");
            DropTable("dbo.Diagram");
            DropTable("dbo.Annotation");
        }
    }
}
