using System.ComponentModel.DataAnnotations;
using ApplicationCore.Models.QuizAggregate;

public class NewQuizDto
{   
    [Required]
    [Length(minimumLength: 3, maximumLength: 200)]
    public string Title { get; set; }

    public List<QuizItem> Items {get;set;}
}