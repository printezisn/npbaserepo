namespace NPBaseRepo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Movies",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(maxLength: 500),
                    DirectorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.DirectorId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropTable("dbo.Movies");
            DropTable("dbo.Directors");
        }
    }
}
