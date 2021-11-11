namespace QandA.Data.Models;

public record QuestionGetManyResponse(int QuestionId, string Title, string Content, string UserName, DateTime Created);

//public class QuestionGetManyResponse
//{
//    public int QuestionId { get; set; }
//    public string Title { get; set; }
//    public string Content { get; set; }
//    public string UserName { get; set; }
//    public DateTime Created { get; set; }
//}