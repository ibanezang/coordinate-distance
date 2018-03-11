using System;
using System.Linq;
using CoordinateDistance.CustomerProblem;
using CoordinateDistance.CustomerProblem.Data;
using CoordinateDistance.CustomerProblem.Log;
using CoordinateDistance.Library.Formula;
using Microsoft.Extensions.DependencyInjection;

namespace CoordinateDistance.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var companyCoordinate = new Coordinate(53.339428, -6.257664);
            var maxDistanceFromCompany = 100;
            var fileName = "customerfile.txt";

            var logger = new InMemoryLogger();
            var serviceProvider = BuildDi(logger);
            var app = serviceProvider.GetRequiredService<ICustomerInvitationApp>();

            var eligibleCustomers = app.GetEligibleCustomer(companyCoordinate, maxDistanceFromCompany, fileName);

            foreach (var customer in eligibleCustomers.OrderBy(x => x.Customer.UserId))
            {
                Console.WriteLine($"User Id: {customer.Customer.UserId} - Name: {customer.Customer.Name} - Distance: {customer.DistanceInKm} kms");
            }

            Console.WriteLine("\n\n\n\nPrint Error Log :");
            foreach (var debugMessage in logger.LogItems.Where(x => x.LogLevel == LogLevel.Error))
            {
                Console.WriteLine(debugMessage.GetLogMessage());
            }

            Console.WriteLine("\n\n\n\nPrint Debug Log :");
            foreach (var debugMessage in logger.LogItems.Where(x => x.LogLevel == LogLevel.Debug))
            {
                Console.WriteLine(debugMessage.GetLogMessage());
            }
        }

        private static IServiceProvider BuildDi(ILogger logger)
        {
            var services = new ServiceCollection();

            //Runner is the custom class
            services.AddTransient<ICustomerInvitationApp, CustomerInvitationApp>();
            services.AddSingleton<ICustomerDataReader, CustomerDataReader>();
            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton(logger);
            services.AddSingleton<IGeoFormula, GeoFormula>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

    }
}
