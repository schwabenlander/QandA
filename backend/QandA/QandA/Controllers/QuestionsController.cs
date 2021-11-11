using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QandA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;

    public QuestionsController(IDataRepository dataRepository)
    {
        this._dataRepository = dataRepository;
    }

    [HttpGet]
    public IEnumerable<QuestionGetManyResponse> GetQuestions()
    {
        var questions = _dataRepository.GetQuestions();
        return questions;
    }
}
