using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacAzureFunctionsExample.Services;

public class RandomNumberService : IRandomNumberService, IDisposable
{
    private readonly ILogger _logger;

    public RandomNumberService(ILogger logger)
    {
        _logger = logger;
    }

    public double GetDouble()
    {
        _logger.LogInformation("RandomNumberService is about to do work...");
        return Random.Shared.NextDouble();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _logger.LogInformation("RandomNumberService was disposed.");
    }
}
