namespace Task2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdfd : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.VoteLogs",
                c => new
                    {
                        AutoId = c.Int(nullable: false, identity: true),
                        VoteForId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        UserName = c.String(),
                        Vote = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AutoId);
            
            
        }
        
        public override void Down()
        {
           
            DropTable("dbo.VoteLogs");
           
        }
    }
}
