using System;
using System.Collections.Generic;
using CoordinateDistance.CustomerProblem.Data;
using CoordinateDistance.CustomerProblem.Log;
using CoordinateDistance.Library.Formula;

namespace CoordinateDistance.CustomerProblem
{
    public interface ICustomerInvitationApp
    {
        List<CustomerDistance> GetEligibleCustomer(
            Coordinate pointRef,
            double withinDistanceKm,
            string filePath);
    }
    public class CustomerInvitationApp : ICustomerInvitationApp
    {
        private readonly ICustomerDataReader _customerDataReader;
        public CustomerInvitationApp(ICustomerDataReader customerDataReader)
        {
            _customerDataReader = customerDataReader;
        }

        public List<CustomerDistance> GetEligibleCustomer(
            Coordinate pointRef, 
            double withinDistanceKm, 
            string filePath)
        {
            var eligibleCustomers = new List<CustomerDistance>(); 
            var customerDistances = _customerDataReader.GetCustomerDistancesFromFile(pointRef, filePath);

            if (customerDistances == null || customerDistances.Count == 0)
            {
                return eligibleCustomers;
            }

            foreach (var customerDistance in customerDistances)
            {
                if (customerDistance.DistanceInKm <= withinDistanceKm)
                {
                    eligibleCustomers.Add(customerDistance);
                }
            }

            return eligibleCustomers;
        }
    }
}
