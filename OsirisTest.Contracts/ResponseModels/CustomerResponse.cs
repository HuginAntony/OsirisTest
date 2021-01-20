using System;

namespace OsirisTest.Contracts.ResponseModels
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal FirstDepositAmount { get; set; }

        public DateTime LastUpdateDateTime { get; set; }
    }
}
