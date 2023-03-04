using ConsoleTables;
using LendingPlatform.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using LendingPlatform.Service.Behaviours;
using FluentValidation;
using LendingPlatform.Service.Models;
using LendingPlatform.Service.Requests;
using LendingPlatform.Service.Rules;
using LendingPlatform.Service.Behaviours.LendingPlatform.Service.Behaviours;

public class Program
{
    private readonly ILogger<Program> _logger;
    private readonly IMediator _mediator;

    public Program(ILogger<Program> logger, IMediator mediator) => (_logger, _mediator) = (logger, mediator);

    public void Run(string[] args)
    {
        try
        {
            var result = _mediator.Send(new LoanApplicationRequest(args)).Result;

            Display(result);


        } catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
        }

    }

    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Please enter loan parameter values.");
            Console.Read();
        }

        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<Program>().Run(args);
    }

    static IHostBuilder CreateHostBuilder(string[] args)
    {        
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddMediatR(Assembly.GetExecutingAssembly());
                services.AddScoped(typeof(IPipelineBehavior<LoanApplicationRequest, LoanApplicationResponse>), typeof(RulesBehaviour<LoanApplicationRequest, LoanApplicationResponse>));
                services.AddScoped(typeof(IPipelineBehavior<LoanApplicationRequest, LoanApplicationResponse>), typeof(LoggingBehaviour<LoanApplicationRequest, LoanApplicationResponse>));
                services.AddScoped<IValidator<LoanApplicationRequest>, LoanApplicationInputsRules>();
                services.AddScoped<IValidator<LoanApplicationRequest>, LoanApplicationRules>();
                services.AddScoped<Program>();
                services.AddSingleton<ILoanApplicationData<LoanApplication>, LoanApplicationData>();
            });
    }

    static void Display(LoanApplicationResponse metrics)
    {
        var tableHeader = new ConsoleTable("Loan Application Success", "Loan Application Failure Reason");

        tableHeader.AddRow(metrics.Success.ToString(),
                    metrics.Success == false ? metrics.ErrorMessage : "N/A");

        tableHeader.Write();

        var tableFooter = new ConsoleTable("Successful Loan Applications", "Unsuccessful Loan Applications", "Total Loans Value", "Average LTV");

        tableFooter.AddRow(metrics.TotalLoanApplications.Count(x => x.Success == true).ToString(),
                    metrics.TotalLoanApplications.Count(x => x.Success == false).ToString(),
                    metrics.TotalLoansValue,
                    metrics.AverageLoanToValue);

        tableFooter.Write();
    }
}
