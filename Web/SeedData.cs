using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            
            
            QuizItem item1 = new QuizItem(id:1, question: "2+4", correctAnswer: "6", incorrectAnswers: ["5","7", "8"]);
            QuizItem item2 = new QuizItem(id:2, question: "2*4", correctAnswer: "8", incorrectAnswers: ["4","6", "9"]);
            QuizItem item3 = new QuizItem(id:3, question: "8/2", correctAnswer: "4", incorrectAnswers: ["5","7", "8"]);

            quizItemRepo?.Add(item1);
            quizItemRepo?.Add(item2);
            quizItemRepo?.Add(item3);

            Quiz quiz = new(id: 1, title: "Matematyka", items: [item1, item2, item3]);

            quizRepo?.Add(quiz);

            item1 = new QuizItem(id:4, question:"What colors is AMD", correctAnswer:"Red", incorrectAnswers: ["Blue", "Green", "Yellow"]);
            item2 = new QuizItem(5, "What colors is Intel", new List<string> {"Red", "Green", "Yellow"}, "Blue");
            item3 = new QuizItem(6, "What colors is Nvidia", new List<string> {"Blue", "Red", "Yellow"}, "Green");

            quizItemRepo?.Add(item1);
            quizItemRepo?.Add(item2);
            quizItemRepo?.Add(item3);

            quiz = new(id: 2, title: "Manufacturers", items: [item1, item2, item3]);

            quizRepo?.Add(quiz);
        }
    }
}