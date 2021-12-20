namespace QUIZ.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepText = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QstText = c.String(),
                        IsMultiple = c.Boolean(nullable: false),
                        NumOrder = c.Int(nullable: false),
                        QuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizzs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.Quizzs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        QuizCategory = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPWD = c.String(nullable: false),
                        TypeUtilisateur = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThemeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuizId", "dbo.Quizzs");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "QuizId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Themes");
            DropTable("dbo.Users");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
