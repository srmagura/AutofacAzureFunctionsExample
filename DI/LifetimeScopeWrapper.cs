using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAzureFunctionsExample.DI;

internal sealed class LifetimeScopeWrapper : IDisposable
{
    public ILifetimeScope Scope { get; }

    public LifetimeScopeWrapper(IContainer container)
    {
        Scope = container.BeginLifetimeScope();
    }

    public void Dispose()
    {
        Scope.Dispose();
    }
}
