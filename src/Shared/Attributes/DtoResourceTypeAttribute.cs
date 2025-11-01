using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DtoResourceTypeAttribute(Type resourceType) : Attribute
{
    public Type ResourceType { get; } = resourceType ?? throw new ArgumentNullException(nameof(resourceType));
}
