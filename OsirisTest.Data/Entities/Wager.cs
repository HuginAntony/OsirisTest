using System;
using System.Collections.Generic;

#nullable disable

namespace OsirisTest.Data
{
    public partial class Wager
    {
        public Guid WagerId { get; set; }
        public int CustomerId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? WagerDateTime { get; set; }
        public bool IsValid { get; set; }
        public DateTime InsertedDateTime { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
