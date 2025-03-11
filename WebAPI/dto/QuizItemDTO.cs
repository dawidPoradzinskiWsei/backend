using ApplicationCore.Models.QuizAggregate;

public class QuizItemDto
{
    public int Id {get;set;}
    public string Question {get;set;}
    public List<string> Options {get;set;}

    public static QuizItemDto of(QuizItem quiz)
    {

        List<string> connectedList = quiz.IncorrectAnswers;
        connectedList.Add(quiz.CorrectAnswer);

        List<string> newList = new List<string>();

        Random random = new Random();

        int nr = random.Next(0, connectedList.Count);

        for(int i = 0; i < connectedList.Count; i++)
        {
            newList.Add(connectedList[nr]);
            nr++;
            if(nr == connectedList.Count)
            {
                nr = 0;
            }
        }
    
        
        return new QuizItemDto 
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Options = newList
        };
        
    }
}

