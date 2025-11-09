using AutoMapper;
using BineshSoloution.DbContexts;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Interfaces.Products;
using BineshSoloution.Interfaces.Sales;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BineshSoloution.Controllers;

public partial class AppControllerBase : ControllerBase
{
    [AutoInject] protected readonly ApplicationDbContext _appDbContext = default!;
    [AutoInject] protected readonly ApplicationIdentityDbContext _appIdentityDbContext = default!;
    [AutoInject] protected readonly IMapper _mapper = default!;
    [AutoInject] protected readonly IPublishEndpoint _publishEndpoint;
    [AutoInject] protected readonly AppSettings _appSettings = default!;
    [AutoInject] protected readonly ISalesService _SalesService = default!;
    [AutoInject] protected readonly IAccountService _AccountService = default!;
    [AutoInject] protected readonly IProductService _ProductService = default!;

}
