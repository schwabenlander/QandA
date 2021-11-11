namespace QandA.Data.Models;

public record AnswerGetResponse(int AnswerId, string Content, string UserName, DateTime Created);

//public class AnswerGetResponse
//{
//    public int AnswerId { get; set; }
//    public string Content { get; set; }
//    public string UserName { get; set; }
//    public DateTime Created { get; set; }
//}
