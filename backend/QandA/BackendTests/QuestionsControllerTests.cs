﻿using System;
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

    [Fact]
    public async void GetQuestion_WhenQuestionNotFound_Returns404()
    {
        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository.Setup(repo => repo.GetQuestionAsync(1))
            .Returns(() => Task.FromResult(default(QuestionGetSingleResponse)));

        var mockQuestionCache = new Mock<IQuestionCache>();
        mockQuestionCache.Setup(cache => cache.Get(1))
            .Returns(() => null);

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(config =>
            config[It.IsAny<string>()]).Returns("some setting");

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            mockQuestionCache.Object,
            null,
            mockConfigurationRoot.Object);

        var result = await questionsController.GetQuestion(1);

        var actionResult = Assert.IsType<ActionResult<QuestionGetSingleResponse>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async void GetQuestion_WhenQuestionIsFound_ReturnsQuestion()
    {
        var mockQuestion = new QuestionGetSingleResponse
        {
            QuestionId = 1,
            Title = "Test"
        };

        var mockDataRepository = new Mock<IDataRepository>();
        mockDataRepository.Setup(repo => repo.GetQuestionAsync(1))
            .Returns(() => Task.FromResult(mockQuestion));

        var mockQuestionCache = new Mock<IQuestionCache>();
        mockQuestionCache.Setup(cache => cache.Get(1))
            .Returns(() => mockQuestion);

        var mockConfigurationRoot = new Mock<IConfigurationRoot>();
        mockConfigurationRoot.SetupGet(config =>
            config[It.IsAny<string>()]).Returns("some setting");

        var questionsController = new QuestionsController(
            mockDataRepository.Object,
            mockQuestionCache.Object,
            null,
            mockConfigurationRoot.Object);

        var result = await questionsController.GetQuestion(1);

        var actionResult = Assert.IsType<ActionResult<QuestionGetSingleResponse>>(result);
        var questionResult = Assert.IsType<QuestionGetSingleResponse>(actionResult.Value);
        Assert.Equal(1, questionResult.QuestionId);
    }
}
