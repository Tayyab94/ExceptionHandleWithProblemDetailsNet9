using Microsoft.AspNetCore.Http.Features;
using OpenQA.Selenium;
using ProblemDetailHandleError.ExHandlers;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Services for using Problem Detail
//builder.Services.AddProblemDetails();

// Customizing Problem Details

#region Customizing Problem Details Region
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
    };
});

#endregion

//This customization adds the request path, a request ID, and a trace ID to every Problem Details response, enhancing debuggability and traceability of errors.


//  Check the ExHandler Folder for its Implementation
builder.Services.AddExceptionHandler<NewCustomHandlerException>();

var app = builder.Build();

// Converts unhandled exceptions into Problem Details responses
app.UseExceptionHandler();



// C# 9 has introduce this new way to Map Exception to the Status COde,, we can 
//app.UseExceptionHandler(new ExceptionHandlerOptions
//{
//    StatusCodeSelector = ex => ex switch
//    {
//        ArgumentException => StatusCodes.Status400BadRequest,
//        NotFoundException => StatusCodes.Status404NotFound,

//        _ => StatusCodes.Status500InternalServerError
//    }
//});

//Returns the Problem Details response for (empty) non-successful responses
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
