//using Ems.Application.Common.Behaviours;
//using Ems.Application.Common.Interfaces;
//using Ems.Application.Employee.Commands.CreateEmployee;
//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;

//namespace Ems.Application.UnitTests.Common.Behaviours;

//public class RequestLoggerTests
//{
//    private Mock<ILogger<CreateEmployeeCommand>> _logger = null!;
//    private Mock<ICurrentUserService> _currentUserService = null!;

//    [SetUp]
//    public void Setup()
//    {
//        _logger = new Mock<ILogger<CreateEmployeeCommand>>();
//        _currentUserService = new Mock<ICurrentUserService>();
//    }

//    [Test]
//    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
//    {
//        _currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

//        var requestLogger = new LoggingBehaviour<CreateEmployeeCommand>(_logger.Object, _currentUserService.Object);

//        await requestLogger.Process(new CreateEmployeeCommand { ListId = 1, Title = "title" }, new CancellationToken());

//        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
//    }

//    [Test]
//    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
//    {
//        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUserService.Object, _identityService.Object);

//        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

//        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
//    }
//}
