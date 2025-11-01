using AutoMapper;
using DataBaseManager.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseManager.Controllers;

public partial class AppControllerBase : ControllerBase
{
    [AutoInject] protected readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] protected readonly IMapper _mapper = default!;
    [AutoInject] protected readonly ILogger<SalesController> _logger = default!;
}
