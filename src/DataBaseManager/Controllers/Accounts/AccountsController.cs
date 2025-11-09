using BineshSoloution.Dtos;
using BineshSoloution.Dtos.Account;
using BineshSoloution.Dtos.Sales;
using BineshSoloution.Exceptions;
using BineshSoloution.Extensions;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Interfaces.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BineshSoloution.Controllers.Accounts;

[ApiController]
[Route("api/[controller]/[action]")]
public partial class AccountsController : AppControllerBase
{
    [AutoInject] protected readonly ILogger<AccountsController> _logger = default!;

    [HttpGet, EnableQuery]
    public IQueryable<SalesDto> Get()
    {
        return _appDbContext.Sales.Project<SalesDto>();
    }
    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<AccountDto>>>> GetAccounts(ODataQueryOptions<AccountDto> odataQuery, CancellationToken cancellationToken)
    {
        try
        {
            var query = (IQueryable<AccountDto>)odataQuery.ApplyTo(Get(), ignoreQueryOptions: AllowedQueryOptions.Top | AllowedQueryOptions.Skip);

            var totalCount = await query.LongCountAsync(cancellationToken);

            if (odataQuery.Skip is not null)
                query = query.Skip(odataQuery.Skip.Value);

            if (odataQuery.Top is not null)
                query = query.Take(odataQuery.Top.Value);

            var res = new PagedResult<AccountDto>(await query.ToArrayAsync(cancellationToken), totalCount);

            await _publishEndpoint.Publish(res, cancellationToken);

            return ApiResponse<PagedResult<AccountDto>>.Success("Accounts fetched successfully", System.Net.HttpStatusCode.OK, res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting Accounts");
            return ApiResponse<PagedResult<AccountDto>>.Success($"Failed to fetched Accounts \n chech logs...", System.Net.HttpStatusCode.OK);

        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AccountDto>>> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var dto = await _AccountService.GetByIdAsync(id, cancellationToken)
                ?? throw new ResourceNotFoundException($"Account record with id: {id} not found!");

            return ApiResponse<AccountDto>.Success("Account fetched successfully", HttpStatusCode.OK, dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Account");
            return ApiResponse<AccountDto>.Fail($"Failed to fetch Account {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AccountDto>>> Create(AccountDto dto, CancellationToken cancellationToken)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await _AccountService.CreateAsync(dto, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ApiResponse<AccountDto>.Success("Account added successfully", HttpStatusCode.OK, result);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error adding Account");
            return ApiResponse<AccountDto>.Fail($"Failed to add Account : {ex.Message}", HttpStatusCode.InternalServerError);
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    [HttpPut]
    public async Task<ActionResult<ApiResponse<AccountDto>>> Update(AccountDto dto, CancellationToken cancellationToken)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await _AccountService.UpdateAsync(dto, cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ApiResponse<AccountDto>.Success("Account updated successfully", HttpStatusCode.OK, result);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error updating Account");
            return ApiResponse<AccountDto>.Fail("Failed to update Account", HttpStatusCode.InternalServerError);
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
            await _AccountService.DeleteAsync(id, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return ApiResponse.Success("Account deleted successfully", HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Error deleting Account");
            return ApiResponse.Fail("Failed to delete Account", HttpStatusCode.InternalServerError);
        }
        finally
        {
            await transaction.DisposeAsync();

        }
    }
}
