using System.Text.Json.Serialization;
using ApplicationCore.Models.QuizAggregate;

public class QuizDTO 
{
    public int Id {get;set;}
    public string Title {get;set;}

    public List<QuizItemDto> Items {get;set;}

    public static QuizDTO of(Quiz quiz)
    {
    
        return new QuizDTO
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Items = quiz.Items.Select(item => QuizItemDto.of(item)).ToList()
        };

    }
}