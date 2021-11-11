namespace QandA.Data.Models;

public record QuestionGetSingleResponse(
    int QuestionId,
    string Title,
    string Content,
    string UserName,
    string UserId,
    DateTime Created,
    IEnumerable<AnswerGetResponse> Answers
);
