using AutoMapper;
using BineshSoloution.DbContexts;
using BineshSoloution.Enum;
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


    protected float CalculateGrowth(float current, float previous)
    {
        if (previous == 0) return 0;
        return (current - previous) / previous;
    }
    protected DateTime GetTimeFrameStart(DateTime date, TimeFrameUnit unit)
    {
        return unit switch
        {
            TimeFrameUnit.Day => date.Date,

            TimeFrameUnit.Week => date.AddDays(-(int)date.DayOfWeek).Date, // Sunday-start week

            TimeFrameUnit.Month => new DateTime(date.Year, date.Month, 1),

            TimeFrameUnit.Quarter =>
                new DateTime(date.Year, ((date.Month - 1) / 3) * 3 + 1, 1),

            TimeFrameUnit.Year => new DateTime(date.Year, 1, 1),

            _ => date.Date
        };
    }
}
