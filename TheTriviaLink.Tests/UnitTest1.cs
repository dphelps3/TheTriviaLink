using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TriviaApp.Controllers;
using DataAccess;
using DataTransfer;
using TriviaLink.Services;

namespace TheTriviaLink.Tests;

public class GameControllerTests
{
    private readonly Mock<IGamesDao> _mockGamesDao;
    private readonly Mock<ICodeGeneratorService> _mockCodeGeneratorService;

    public GameControllerTests()
    {
        _mockGamesDao = new Mock<IGamesDao>();
        _mockCodeGeneratorService = new Mock<ICodeGeneratorService>();
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfGames()
    {
        // Arrange Mock Data
        var mockGamesDao = new Mock<IGamesDao>();
        mockGamesDao.Setup(repo => repo.GetAllGamesAsync()).ReturnsAsync(GetSampleGames());

        var mockCodeGeneratorService = new Mock<ICodeGeneratorService>();
        mockCodeGeneratorService.Setup(service => service.GenerateUniqueCode()).ReturnsAsync("GeneratedCode");

        var controller = new GameController(mockGamesDao.Object, mockCodeGeneratorService.Object);

        var result = await controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Game>>(viewResult.ViewData.Model);
        var gamesList = (List<Game>)model;
        Assert.Single(gamesList);
        Assert.Equal("GeneratedCode", gamesList[0].GameCode);
    }

    private List<Game> GetSampleGames()
    {
        string gameDateString = "2025-07-31";

        return new List<Game>
        {
            new Game { GameID = 1, 
                       GameDay = DateTime.Parse(gameDateString), 
                       GameFormat = 1,
                       GameTheme = "Sports",
                       GameLocation = "Miller Park",
                       MasterFirstName = "Daphne", 
                       MasterLastName = "Vasquez",
                       GameCode = _mockCodeGeneratorService.Object.GenerateUniqueCode().Result
            }
        };
    }
}