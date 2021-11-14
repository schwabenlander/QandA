using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace QandA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;
    private readonly IQuestionCache _questionCache;
    private readonly IHttpClientFactory _clientFactory;
    private readonly string _auth0UserInfo;

    public QuestionsController(IDataRepository dataRepository, 
        IQuestionCache questionCache, 
        IHttpClientFactory clientFactory, 
        IConfiguration configuration)
    {
        _dataRepository = dataRepository;
        _questionCache = questionCache;
        _clientFactory = clientFactory;
        _auth0UserInfo = $"https://{configuration["Auth0:Domain"]}/userinfo";
    }

    private async Task<string> GetUserNameAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, _auth0UserInfo);
        request.Headers.Add("Authorization", Request.Headers["Authorization"].First());

        var client = _clientFactory.CreateClient();

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return user.Name;
        }
        else
            return string.Empty;
    }

    [HttpGet]
    public IEnumerable<QuestionGetManyResponse> GetQuestions(
        string search, 
        bool includeAnswers, 
        int page = 1,
        int pageSize = 20)
    {
        if (string.IsNullOrEmpty(search))
        {
            if (includeAnswers)
                return _dataRepository.GetQuestionsWithAnswers();
            else
                return _dataRepository.GetQuestions();
        }
        else
            return _dataRepository.GetQuestionsBySearchWithPaging(search, page, pageSize);
    }

    [HttpGet("unanswered")]
    public async Task<IEnumerable<QuestionGetManyResponse>> GetUnansweredQuestions()
    {
        return await _dataRepository.GetUnansweredQuestionsAsync();
    }

    [HttpGet("{questionId}")]
    public async Task<ActionResult<QuestionGetSingleResponse>> GetQuestion(int questionId)
    {
        var question = _questionCache.Get(questionId);

        if (question == null)
        {
            question = await _dataRepository.GetQuestionAsync(questionId);

            if (question == null)
                return NotFound();

            _questionCache.Set(question);
        }

        return question;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<QuestionGetSingleResponse>> PostQuestion(QuestionPostRequest questionPostRequest)
    {
        var savedQuestion = _dataRepository.PostQuestion(
            new QuestionPostFullRequest()
            {
                Title = questionPostRequest.Title,
                Content = questionPostRequest.Content,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserName = await GetUserNameAsync(),
                Created = DateTime.UtcNow
            });

        return CreatedAtAction(nameof(GetQuestion), 
            new { questionId = savedQuestion.QuestionId }, 
            savedQuestion);
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpPut("{questionId}")]
    public ActionResult<QuestionGetSingleResponse> PutQuestion(int questionId, QuestionPutRequest questionPutRequest)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question == null)
            return NotFound();

        questionPutRequest.Title = !string.IsNullOrEmpty(questionPutRequest.Title) ? 
            questionPutRequest.Title : question.Title;
        questionPutRequest.Content = !string.IsNullOrEmpty(questionPutRequest.Content) ? 
            questionPutRequest.Content : question.Content;

        var savedQuestion = _dataRepository.PutQuestion(questionId, questionPutRequest);

        _questionCache.Remove(savedQuestion.QuestionId);

        return savedQuestion;
    }

    [Authorize(Policy = "MustBeQuestionAuthor")]
    [HttpDelete("{questionId}")]
    public ActionResult DeleteQuestion(int questionId)
    {
        var question = _dataRepository.GetQuestion(questionId);

        if (question == null)
            return NotFound();

        _dataRepository.DeleteQuestion(questionId);

        _questionCache.Remove(questionId);

        return NoContent();
    }

    [Authorize]
    [HttpPost("answer")]
    public async Task<ActionResult<AnswerGetResponse>> PostAnswer(AnswerPostRequest answerPostRequest)
    {
        var questionExists = _dataRepository.QuestionExists(answerPostRequest.QuestionId.Value);

        if (!questionExists)
            return NotFound();

        var savedAnswer = _dataRepository.PostAnswer( new AnswerPostFullRequest()
        {
            QuestionId = answerPostRequest.QuestionId.Value,
            Content = answerPostRequest.Content,
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
            UserName = await GetUserNameAsync(),
            Created = DateTime.UtcNow
        });

        _questionCache.Remove(answerPostRequest.QuestionId.Value);

        return savedAnswer;
    }
}
