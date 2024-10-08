﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using ApiEnum = Testeger.Shared.DTOs.Enumerations;
using DomainEnum = Testeger.Domain.Enumerations;

namespace Testeger.Application.MappingProfiles;

public class EnumerationMappingProfile : Profile
{
    public EnumerationMappingProfile()
    {
        CreateMap<ApiEnum.PriorityLevel, DomainEnum.PriorityLevel>()
            .ConvertUsingEnumMapping();

        CreateMap<ApiEnum.RequestStatus, DomainEnum.RequestStatus>()
            .ConvertUsingEnumMapping();

        CreateMap<ApiEnum.TestCaseStatus, DomainEnum.TestCaseStatus>()
            .ConvertUsingEnumMapping();
    }
}
