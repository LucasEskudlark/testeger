namespace UnitTests.Application.Services;

public class BaseServiceTests
{
    protected readonly Mock<IUnitOfWork> _unitOfWork;
    protected readonly Mock<IMapper> _mapper;
    protected readonly Fixture _fixture;

    public BaseServiceTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _fixture = new();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
    }
}
