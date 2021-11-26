﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacAzureFunctionsExample.DI;
using AutofacAzureFunctionsExample.Functions;
using AutofacAzureFunctionsExample.Services;
using FunctionApp.DI;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(AutofacAzureFunctionsExample.Startup))]

namespace AutofacAzureFunctionsExample;

internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Use IServiceCollection Add extension methods here, e.g.
        builder.Services.AddDataProtection();

        builder.Services.AddSingleton(GetContainer(builder.Services));
        builder.Services.AddScoped<LifetimeScopeWrapper>();

        builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IJobActivator), typeof(AutofacJobActivator)));
        builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IJobActivatorEx), typeof(AutofacJobActivator)));
    }

    private static IContainer GetContainer(IServiceCollection serviceCollection)
    {
        var builder = new ContainerBuilder();
        builder.Populate(serviceCollection);
        builder.RegisterModule<LoggerModule>();

        // Register all classes in the Functions namespace
        builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            .InNamespaceOf<Function1>();

        // Here you can register other dependencies with the ContainerBuilder like normal
        builder.RegisterType<RandomNumberService>().As<IRandomNumberService>();

        return builder.Build();
    }
}
