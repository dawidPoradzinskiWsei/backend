using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;

public class AutoMapperProfiles: Profile
{
    
    public AutoMapperProfiles()
    {
        CreateMap<QuizItem, QuizItemDto>()
            .ForMember(
                q => q.Options,
                op => op.MapFrom(i => new List<string>(i.IncorrectAnswers) { i.CorrectAnswer }));
        CreateMap<Quiz, QuizDTO>()
            .ForMember(
                q => q.Items,
                op => op.MapFrom<List<QuizItem>>(i => i.Items)
            );
        CreateMap<NewQuizDto, Quiz>();
        CreateMap<List<QuizItemUserAnswer>, FeedbackDTO>()
            .ForMember(
                q => q.QuizId,
                op => op.MapFrom(quizList => quizList[0].QuizId)
            )
            .ForMember(
                q => q.UserId,
                op => op.MapFrom(quizList => quizList[0].UserId)
            )
            .ForMember(
                q => q.TotalQuestions,
                op => op.MapFrom(quizList => quizList.Count)
            )
            .ForMember(
                q => q.Details,
                op => op.MapFrom(quizList => quizList.Select(quiz =>
                    new {
                        question = quiz.QuizItem.Question,
                        answer = quiz.Answer,
                        isCorrect = quiz.IsCorrect()
                    }
                ))
            );
    }
}