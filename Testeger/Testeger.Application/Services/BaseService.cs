using AutoMapper;
using Testeger.Infra.UnitOfWork;

namespace Testeger.Application.Services;

public abstract class BaseService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    protected static string GenerateGuid() => Guid.NewGuid().ToString();
}
