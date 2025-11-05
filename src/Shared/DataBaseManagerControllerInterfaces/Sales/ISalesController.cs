using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Shared.Dtos;
using Shared.Dtos.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataBaseManagerControllerInterfaces.Sales;

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
