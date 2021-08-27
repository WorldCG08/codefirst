namespace Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddManyToManyBetweenGenreAndVideo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoGenres",
                c => new
                    {
                        Video_Id = c.Int(nullable: false),
                        Genre_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.Video_Id, t.Genre_Id })
                .ForeignKey("dbo.Videos", t => t.Video_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Video_Id)
                .Index(t => t.Genre_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.VideoGenres", "Video_Id", "dbo.Videos");
            DropIndex("dbo.VideoGenres", new[] { "Genre_Id" });
            DropIndex("dbo.VideoGenres", new[] { "Video_Id" });
            DropTable("dbo.VideoGenres");
        }
    }
}
