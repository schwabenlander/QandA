using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;
using Moq;
using QandA.Controllers;
using QandA.Data;
using QandA.Data.Models;

namespace BackendTests;

public class QuestionsControllerTests
{
    [Fact]
    public void GetQuestions_WhenNoParameters_ReturnsAllQuestions()
    {
        var mockQuestions = new List<QuestionGetManyResponse>();
        for (int i = 1; i <= 10; i++)
        {
            mockQuestions.Add(new QuestionGetManyResponse
            {
                QuestionId = i,
                Title = $"Test Title {i}",
                Content = $"Test Content {i}",
                UserName = "User1",
                Answers = new List<AnswerGetResponse>()
            });
        }

        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository.Setup(repo => repo.GetQuestions())
            .Returns(mockQuestions.AsEnumerable());

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(config =>
            config[It.IsAny<string>()]).Returns("some setting");

        var questionsController = new QuestionsController(
            mockDataRepository.Object, 
            null, 
            null, 
            mockConfigurationRoot.Object);

        var result = questionsController.GetQuestions(null, false);

        Assert.Equal(10, result.Count());
        mockDataRepository.Verify(mock => mock.GetQuestions(), Times.Once);
    }

    [Fact]
    public void GetQuestions_WhenHaveSearchParameter_ReturnsCorrectQuestions()
    {
        var mockQuestions = new List<QuestionGetManyResponse>
        {
            new QuestionGetManyResponse
            {
                QuestionId = 1,
                Title = "Test",
                Content = "Test Content",
                UserName = "User1",
                Answers = new List<AnswerGetResponse>()
            }
        };

        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository.Setup(repo => repo.GetQuestionsBySearchWithPaging("Test", 1, 20))
            .Returns(mockQuestions.AsEnumerable());

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(config =>
            config[It.IsAny<string>()]).Returns("some setting");

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            null,
            null,
            mockConfigurationRoot.Object);

        var result = questionsController.GetQuestions("Test", false);

        Assert.Single(result);
        mockDataRepository.Verify(mock => mock.GetQuestionsBySearchWithPaging("Test", 1, 20), Times.Once);
    }
}
