namespace QUIZ.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementModelQuizz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizzs", "ThemeName", c => c.String());
            DropColumn("dbo.Quizzs", "QuizCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quizzs", "QuizCategory", c => c.String());
            DropColumn("dbo.Quizzs", "ThemeName");
        }
    }
}
