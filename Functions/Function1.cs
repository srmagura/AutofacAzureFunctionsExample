using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AutofacAzureFunctionsExample.Services;

namespace AutofacAzureFunctionsExample.Functions;

public class Function1
{
    private readonly IRandomNumberService _randomNumberService;

    public Function1(IRandomNumberService randomNumberService)
    {
        _randomNumberService = randomNumberService;
    }

    // Call this by going to http://localhost:7071/api/Function1 in your web browser
    [FunctionName("Function1")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest request,
        ILogger log
    )
    {
        var number = _randomNumberService.GetDouble();
        log.LogInformation($"Function1 received the number {number}.");

        return new OkObjectResult($"Your random number is {number}.");
    }
}
