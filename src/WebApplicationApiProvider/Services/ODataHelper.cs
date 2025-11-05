using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

public static class ODataHelper
{
    public static ODataQueryOptions<T> BuildQueryOptions<T>(ControllerBase controller, string odataQueryString)
        where T : class
    {
        // 1️⃣ Build EDM model for T
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<T>(typeof(T).Name);
        IEdmModel model = builder.GetEdmModel();

        // 2️⃣ Create HttpContext & ODataFeature to simulate an OData request
        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = $"/odata/{typeof(T).Name}";
        context.Request.QueryString = new QueryString(odataQueryString);
        context.ODataFeature().Model = model;

        // 3️⃣ Build ODataQueryContext and ODataQueryOptions
        var queryContext = new ODataQueryContext(model, typeof(T), new Microsoft.OData.UriParser.ODataPath());
        var queryOptions = new ODataQueryOptions<T>(queryContext, context.Request);

        return queryOptions;
    }
}
