﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _logger;
    private readonly Mock<ILeaveTypeRepository> _mockLeaveTypeRepository;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockLeaveTypeRepository = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _logger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper,
            _mockLeaveTypeRepository.Object, _logger.Object);

        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
