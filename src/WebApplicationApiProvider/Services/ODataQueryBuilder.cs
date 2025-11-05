using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Linq.Expressions;

namespace WebApplicationApiProvider.Services;

public static class ODataQueryBuilder
{
    public static ODataQueryOptions<T> FromExpression<T>(
        ControllerBase controller,
        Expression<Func<T, bool>> filterExpression)
        where T : class
    {
        var filter = ODataExpressionConverter.ToODataFilter(filterExpression);
        return ODataHelper.BuildQueryOptions<T>(controller, "?" + filter);
    }
}
