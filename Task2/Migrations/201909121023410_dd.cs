namespace Task2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyComments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        ThisDateTime = c.DateTime(),
                        CompanyId = c.Int(nullable: false),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CompanyComments");
        }
    }
}
