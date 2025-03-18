using System.ComponentModel.DataAnnotations;

public class NewQuizDto
{
    [Required]
    [Length(minimumLength: 3, maximumLength: 200)]
    public string Title { get; set; }
}