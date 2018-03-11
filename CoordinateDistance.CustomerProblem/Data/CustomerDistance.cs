using System;
using CoordinateDistance.CustomerProblem.Data;

namespace CoordinateDistance.CustomerProblem
{
    public class CustomerDistance
    {
        public CustomerRecord Customer { get; }
        public double DistanceInKm { get; }
        public CustomerDistance(CustomerRecord customer, double distance)
        {
            Customer = customer;
            DistanceInKm = distance;
        }
    }
}
