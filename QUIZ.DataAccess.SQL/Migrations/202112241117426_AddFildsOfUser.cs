namespace QUIZ.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFildsOfUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "TotalPoints", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Avatar");
            DropColumn("dbo.Users", "TotalPoints");
            DropColumn("dbo.Users", "Email");
        }
    }
}
