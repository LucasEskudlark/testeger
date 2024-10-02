using AutoFixture;
using AutoMapper;
using Moq;
using Testeger.Infra.UnitOfWork;

namespace UnitTests.Application.Services;

public class BaseServiceTests
{
    protected readonly Mock<IUnitOfWork> _unitOfWork;
    protected readonly Mock<IMapper> _mapper;
    protected readonly Fixture _fixture = new ();

    public BaseServiceTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
    }
}
