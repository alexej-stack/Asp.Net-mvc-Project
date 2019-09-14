namespace Task2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sds : DbMigration
    {
        public override void Up()
        {
           
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            
            
            DropTable("dbo.UserProfiles");
           
        }
    }
}
