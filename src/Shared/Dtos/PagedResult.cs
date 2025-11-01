using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos;

public class PagedResult<T>
{
    public T[] Items { get; set; } = [];

    public long TotalCount { get; set; }

    [JsonConstructor]
    public PagedResult(T[] items, long totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
