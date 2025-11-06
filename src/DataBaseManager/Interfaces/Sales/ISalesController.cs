using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using DataBaseManager.Dtos;
using DataBaseManager.Dtos.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Interfaces.Sales;

public interface ISalesController : IAppControllerBase
{
    [HttpGet]
    public  Task<ActionResult<ApiResponse<PagedResult<SalesDto>>>> GetSales(ODataQueryOptions<SalesDto> odataQuery, CancellationToken cancellationToken);
    
    [HttpGet]
    public Task<ActionResult<ApiResponse<SalesDto>>> Get(Guid id, CancellationToken cancellationToken);
    
    [HttpPost]
    public Task<ActionResult<ApiResponse<SalesDto>>> Create(SalesDto dto, CancellationToken cancellationToken);
    
    [HttpPut]
    public Task<ActionResult<ApiResponse<SalesDto>>> Update(SalesDto dto, CancellationToken cancellationToken);
    [HttpDelete]
    public Task<ApiResponse> Delete(Guid id, CancellationToken cancellationToken);


}
