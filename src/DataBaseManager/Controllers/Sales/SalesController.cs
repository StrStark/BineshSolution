using AutoMapper;
using DataBaseManager.DbContexts;
using DataBaseManager.Dtos;
using DataBaseManager.Dtos.Inventory;
using DataBaseManager.Dtos.Sales;
using DataBaseManager.Enum;
using DataBaseManager.Exceptions;
using DataBaseManager.Extensions;
using DataBaseManager.Interfaces.Sales;
using DataBaseManager.Models.DataBaseModels.Account;
using DataBaseManager.Models.DataBaseModels.Inventory;
using DataBaseManager.Models.Sales;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class SalesController : AppControllerBase
    {

        [AutoInject] protected readonly ILogger<SalesController> _logger = default!;
        [AutoInject] protected readonly ISalesService _salesService = default!;

        [HttpGet, EnableQuery]
        public IQueryable<SalesDto> Get()
        {
            return _appDbContext.Sales.Project<SalesDto>();
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<SalesDto>>>> GetSales(ODataQueryOptions<SalesDto> odataQuery, CancellationToken cancellationToken)
        {
            try
            {
                var query = (IQueryable<SalesDto>)odataQuery.ApplyTo(Get(), ignoreQueryOptions: AllowedQueryOptions.Top | AllowedQueryOptions.Skip);

                var totalCount = await query.LongCountAsync(cancellationToken);

                if (odataQuery.Skip is not null)
                    query = query.Skip(odataQuery.Skip.Value);

                if (odataQuery.Top is not null)
                    query = query.Take(odataQuery.Top.Value);

                var res = new PagedResult<SalesDto>(await query.ToArrayAsync(cancellationToken), totalCount);

                await _publishEndpoint.Publish(res, cancellationToken);

                return ApiResponse<PagedResult<SalesDto>>.Success("Sales fetched successfully", System.Net.HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding sales");
                return ApiResponse<PagedResult<SalesDto>>.Success($"Failed to fetched sales \n chech logs...", System.Net.HttpStatusCode.OK);

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SalesDto>>> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await _salesService.GetByIdAsync(id, cancellationToken)
                    ?? throw new ResourceNotFoundException($"Sale record with id: {id} not found!");

                return ApiResponse<SalesDto>.Success("Sale fetched successfully", HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sale");
                return ApiResponse<SalesDto>.Fail("Failed to fetch sale", HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<SalesDto>>> Create(SalesDto dto, CancellationToken cancellationToken)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _salesService.CreateAsync(dto, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return ApiResponse<SalesDto>.Success("Sale added successfully", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error adding sale");
                return ApiResponse<SalesDto>.Fail("Failed to add sale", HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<SalesDto>>> Update(SalesDto dto, CancellationToken cancellationToken)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _salesService.UpdateAsync(dto, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return ApiResponse<SalesDto>.Success("Sale updated successfully", HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error updating sale");
                return ApiResponse<SalesDto>.Fail("Failed to update sale", HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id, CancellationToken cancellationToken)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _salesService.DeleteAsync(id, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return ApiResponse.Success("Sale deleted successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error deleting sale");
                return ApiResponse.Fail("Failed to delete sale", HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transaction.DisposeAsync();

            }
        }
    }
}