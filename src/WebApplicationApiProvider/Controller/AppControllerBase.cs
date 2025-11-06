using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shared.DataBaseManagerControllerInterfaces.Sales;
using WebApplicationApiProvider.Controller.Sales;

namespace WebApplicationApiProvider.Controller;

public partial class AppControllerBase : ControllerBase
{
    [AutoInject] protected readonly ISalesController _salesController = default!;
}
