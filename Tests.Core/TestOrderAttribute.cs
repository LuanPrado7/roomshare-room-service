using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]

public class TestOrderAttribute : Attribute
{
    public int Order { get; private set; }

    public TestOrderAttribute(int order) => Order = order;
}
