using System.Globalization;
using System.Linq.Expressions;

namespace WebApplicationApiProvider.Services;

public static class ODataExpressionConverter
{
    public static string ToODataFilter<T>(Expression<Func<T, bool>> expression)
    {
        return "$filter=" + VisitExpression(expression.Body);
    }

    private static string VisitExpression(Expression expr)
    {
        return expr switch
        {
            BinaryExpression binary => VisitBinary(binary),
            MemberExpression member => member.Member.Name,
            ConstantExpression constant => FormatConstant(constant.Value),
            UnaryExpression unary when unary.NodeType == ExpressionType.Convert => VisitExpression(unary.Operand),
            _ => throw new NotSupportedException($"Unsupported expression: {expr.NodeType}")
        };
    }

    private static string VisitBinary(BinaryExpression expr)
    {
        var left = VisitExpression(expr.Left);
        var right = VisitExpression(expr.Right);

        string op = expr.NodeType switch
        {
            ExpressionType.Equal => "eq",
            ExpressionType.NotEqual => "ne",
            ExpressionType.GreaterThan => "gt",
            ExpressionType.GreaterThanOrEqual => "ge",
            ExpressionType.LessThan => "lt",
            ExpressionType.LessThanOrEqual => "le",
            ExpressionType.AndAlso => "and",
            ExpressionType.OrElse => "or",
            _ => throw new NotSupportedException($"Unsupported binary operator: {expr.NodeType}")
        };

        return $"{left} {op} {right}";
    }

    private static string FormatConstant(object? value)
    {
        if (value == null)
            return "null";

        return value switch
        {
            string s => $"'{s}'",
            Guid g => g.ToString(),
            DateTime dt => dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            bool b => b ? "true" : "false",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "null"
        };
    }
}
