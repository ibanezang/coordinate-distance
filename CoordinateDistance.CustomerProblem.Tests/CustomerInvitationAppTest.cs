using System.Collections.Generic;
using CoordinateDistance.CustomerProblem.Data;
using CoordinateDistance.Library.Formula;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CoordinateDistance.CustomerProblem.Tests
{
    public class CustomerInvitationAppTest
    {
        private ICustomerDataReader _customerDataReader;
        private CustomerInvitationApp _customerInvitationApp;

        private Coordinate _testCoordinate = new Coordinate(53.339428, -6.257664);
        private double TestEligibleDistance = 50;

        public CustomerInvitationAppTest()
        {
            _customerDataReader = Substitute.For<ICustomerDataReader>();
            _customerInvitationApp = new CustomerInvitationApp(_customerDataReader);
        }

        [Fact]
        public void GetEligibleCustomer_GetNullCustomerDistances_ShouldReturnEmpty()
        {
            var result = _customerInvitationApp.GetEligibleCustomer(_testCoordinate, TestEligibleDistance, "filePath.txt");
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetEligibleCustomer_GetEmptyCustomerDistances_ShouldReturnEmpty()
        {
            _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, Arg.Any<string>())
                               .Returns(new List<CustomerDistance>());
            var result = _customerInvitationApp.GetEligibleCustomer(_testCoordinate, TestEligibleDistance, "filePath.txt");
            result.Count.ShouldBe(0);
        }

        [Fact]
        public void GetEligibleCustomer_GetSeveralCustomerDistancesData_ShouldReturnEligibleCustomer()
        {
            _customerDataReader.GetCustomerDistancesFromFile(_testCoordinate, Arg.Any<string>())
           .Returns(new List<CustomerDistance>
           {
                new CustomerDistance(new CustomerRecord{
                    UserId = 1,
                    Name = "Rachel"
                }, 10),
                new CustomerDistance(new CustomerRecord{
                    UserId = 2,
                    Name = "Nadia"
                }, 10),
                new CustomerDistance(new CustomerRecord{
                    UserId = 3,
                    Name = "Rudi"
                }, 60),
                new CustomerDistance(new CustomerRecord{
                    UserId = 4,
                    Name = "Susi"
                }, 30),
                new CustomerDistance(new CustomerRecord{
                    UserId = 5,
                    Name = "John"
                }, 50)
            });
            var result = _customerInvitationApp.GetEligibleCustomer(_testCoordinate, TestEligibleDistance, "filePath.txt");
            result.Count.ShouldBe(4);
        }
    }
}
