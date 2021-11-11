namespace QandA.Data.Models;

public record QuestionPostRequest(string Title, string Content, string UserId, string UserName, DateTime Created);
