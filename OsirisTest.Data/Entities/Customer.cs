using System;
using System.Collections.Generic;

#nullable disable

namespace OsirisTest.Data
{
    public partial class Customer
    {
        public Customer()
        {
            Wagers = new HashSet<Wager>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal? FirstDepositAmount { get; set; }
        public decimal? LastWagerAmount { get; set; }
        public DateTime? LastWagerDateTime { get; set; }
        public DateTime InserteDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }

        public virtual ICollection<Wager> Wagers { get; set; }
    }
}
