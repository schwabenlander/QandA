using Microsoft.Extensions.Caching.Memory;

namespace QandA.Data;

public class QuestionCache : IQuestionCache
{
    private MemoryCache _cache;

    private string GetCacheKey(int questionId) => $"Question-{questionId}";

    public QuestionCache()
    {
        _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = 100 });
    }

    public QuestionGetSingleResponse Get(int questionId)
    {
        _cache.TryGetValue(GetCacheKey(questionId), out QuestionGetSingleResponse question);

        return question;
    }

    public void Remove(int questionId)
    {
        _cache.Remove(GetCacheKey(questionId));
    }

    public void Set(QuestionGetSingleResponse question)
    {
        _cache.Set(GetCacheKey(question.QuestionId),
            question,
            new MemoryCacheEntryOptions().SetSize(1));
    }
}
