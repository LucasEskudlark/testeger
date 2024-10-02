using Microsoft.Extensions.Options;
using Moq;
using Testeger.Application.Services.Email;
using Testeger.Application.Services.TestRequest;
using Testeger.Application.Services.User;
using Testeger.Application.Settings;

namespace UnitTests.Application.Services.TestRequest;

public class TestRequestServiceTestBase : BaseServiceTests
{
    protected readonly Mock<IEmailService> _emailService;
    protected readonly Mock<IUserService> _userService;
    protected readonly ITestRequestService _testRequestService;
    private readonly ClientSettings _clientSettings;

    public TestRequestServiceTestBase()
    {
        _emailService = new Mock<IEmailService>();
        _userService = new Mock<IUserService>();
        _clientSettings = new() { BaseAddress = string.Empty };
        IOptions<ClientSettings> options = Options.Create(_clientSettings);

        _testRequestService = new TestRequestService(_unitOfWork.Object, _mapper.Object, _emailService.Object, _userService.Object, options);
    }
}
