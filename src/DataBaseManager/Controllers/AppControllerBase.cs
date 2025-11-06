using AutoMapper;
using DataBaseManager.DbContexts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseManager.Controllers;

public partial class AppControllerBase : ControllerBase
{
    [AutoInject] protected readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] protected readonly ApplicationIdentityDbContext _appIdentityDbContext = default!;
    [AutoInject] protected readonly IMapper _mapper = default!;
    [AutoInject] protected readonly IPublishEndpoint _publishEndpoint;
    [AutoInject] protected readonly AppSettings _appSettings = default!;
    
}
